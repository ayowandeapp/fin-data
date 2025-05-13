using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevHabit.Api.Dtos.Auth;
using DevHabit.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevHabit.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authService.RegisterAsync(requestDto);
                if (!result.Success)
                    return BadRequest(new { Errors = result.Errors });

                //123String*string
                // return Ok(new { Token = result.Token, UserId = result.UserId });
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto requestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authService.LoginAsync(requestDto);
                if (!result.Success)
                    return Unauthorized(new { Errors = result.Errors });
                
                return Ok(result);

            }
            catch (System.Exception e)
            {

                return StatusCode(500, e);
            }
        }

    }
}