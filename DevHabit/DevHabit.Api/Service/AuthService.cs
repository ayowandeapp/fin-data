using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHabit.Api.Dtos.Auth;
using DevHabit.Api.Helpers;
using DevHabit.Api.Interfaces;
using DevHabit.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace DevHabit.Api.Service
{
    public class AuthService(UserManager<AppUser> userManager) : IAuthService
    {
        private readonly UserManager<AppUser> _userManager = userManager;

        public Task<AuthResult> LoginAsync(string email, string Password)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResult> RegisterAsync(UserRegistrationRequestDto requestDto)
        {
            var appUser = new AppUser 
            {
                UserName = requestDto.Username,
                Email = requestDto.Email
            };
            var createdUser = await _userManager.CreateAsync(appUser, requestDto.Password);
            if(!createdUser.Succeeded)
                return new AuthResult { Errors = createdUser.Errors.Select(x => x.Description)};
            //assign the User role
            var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
            if(!roleResult.Succeeded)
            {
                return new AuthResult 
                {
                    Success = false,
                    Errors = roleResult.Errors.Select(x => x.Description)
                };
            }
            
            return new AuthResult 
            {
                Success = true
            };

        }
    }
}