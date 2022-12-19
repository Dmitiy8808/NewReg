using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
		private readonly IAuthenticationService _authenticationService;

		public TokenController(UserManager<User> userManager,
			IAuthenticationService authenticationService)
		{
			_userManager = userManager;
			_authenticationService = authenticationService;
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh(RefreshTokenDto tokenDto)
		{
			if (tokenDto == null)
				return BadRequest(new AuthResponseDto
				{
					IsAuthSuccessful = false,
					ErrorMessage = "Неверный клиентский запрос"
				});

			var principal = _authenticationService
				.GetPrincipalFromExpiredToken(tokenDto.Token);
			var username = principal.Identity.Name;

			var user = await _userManager.FindByEmailAsync(username);
			if (user == null || user.RefreshToken != tokenDto.RefreshToken ||
				user.RefreshTokenExpiryTime <= DateTime.Now)
				return BadRequest(new AuthResponseDto
				{
					IsAuthSuccessful = false,
					ErrorMessage = "Неверный клиентский запрос"
				});

			var token = await _authenticationService.GetToken(user);
			user.RefreshToken = _authenticationService.GenerateRefreshToken();

			await _userManager.UpdateAsync(user);

			return Ok(new AuthResponseDto
			{
				Token = token,
				RefreshToken = user.RefreshToken,
				IsAuthSuccessful = true
			});
		}
    }
}