using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationService _authenticationService;

        public AccountController(UserManager<User> userManager, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _userManager = userManager;
            
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            if (userForRegistrationDto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new User
            {
                UserName = userForRegistrationDto.Email,
                Email = userForRegistrationDto.Email
            };

            var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new ResponseDto {Errors = errors}); 
            }

            await _userManager.AddToRoleAsync(user, "User");

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            return Unauthorized(new AuthResponseDto { ErrorMessage = "Неверные логин или пароль" });
            
            var token = await _authenticationService.GetToken(user);
            
            user.RefreshToken = _authenticationService.GenerateRefreshToken();
			user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
			await _userManager.UpdateAsync(user);

			return Ok(new AuthResponseDto 
			{ 
				IsAuthSuccessful = true, 
				Token = token,
				RefreshToken = user.RefreshToken
			});
        }
    }
}