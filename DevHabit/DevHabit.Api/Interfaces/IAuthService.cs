using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHabit.Api.Dtos.Auth;
using DevHabit.Api.Helpers;
using DevHabit.Api.Models;

namespace DevHabit.Api.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(UserRegistrationRequestDto requestDto);
        Task<AuthResult> LoginAsync(string email, string Password); 
        AuthResult CreateToken(AppUser user);       
    }
}