using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DevHabit.Api.Dtos.Auth;
using DevHabit.Api.Helpers;
using DevHabit.Api.Interfaces;
using DevHabit.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DevHabit.Api.Service
{
    public class AuthService(UserManager<AppUser> userManager, IConfiguration config) : IAuthService
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IConfiguration _config = config;

        public AuthResult CreateToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]);
            var claims = new List<Claim>
            {
                // new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim("id", user.Id)

            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                UserId = user.Id
            };
        }

        public Task<AuthResult> LoginAsync(string email, string Password)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResult> RegisterAsync(UserRegistrationRequestDto requestDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(requestDto.Email);
            if (existingUser != null)
                return new AuthResult { Errors = new[] { "Email already in use" } };
            var appUser = new AppUser
            {
                UserName = requestDto.Username,
                Email = requestDto.Email
            };
            var createdUser = await _userManager.CreateAsync(appUser, requestDto.Password);
            if (!createdUser.Succeeded)
                return new AuthResult { Errors = createdUser.Errors.Select(x => x.Description) };
            //assign the User role
            var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
            if (!roleResult.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    Errors = roleResult.Errors.Select(x => x.Description)
                };
            }

            return CreateToken(appUser);

        }
    }
}