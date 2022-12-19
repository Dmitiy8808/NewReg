using System.Text;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Server.Data;
using Server.Services;
using Server.Services.EmailService;


namespace Server.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<User> userManager, IAuthenticationService authenticationService,
            IEmailSender emailSender)
        {
            _authenticationService = authenticationService;
            _userManager = userManager;
            _emailSender = emailSender;
            
        }
        [HttpPost("register")]
		public async Task<IActionResult> RegisterUser(
			[FromBody] UserForRegistrationDto userForRegistrationDto)
		{
			if (userForRegistrationDto is null || !ModelState.IsValid)
				return BadRequest();

			var user = new User
			{
				UserName = userForRegistrationDto.Email,
				Email = userForRegistrationDto.Email
			};

			var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);
			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);
				return BadRequest(new ResponseDto { Errors = errors });
			}

			await _userManager.AddToRoleAsync(user, "User");

			return StatusCode(201);
		}

        [HttpPost("registerEmail")]
        public async Task<IActionResult> RegisterUserNoPassword(UserForRegistrationDto userForRegistrationDto)
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
            var password = GeneratePassword();
            userForRegistrationDto.Password = password;
            userForRegistrationDto.ConfirmPassword = password;

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

        [HttpPost("ForgotPassword")]
		public async Task<IActionResult> ForgotPassword(
			ForgotPasswordDto forgotPasswordDto)
		{
			var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
			if (user == null)
				return BadRequest("Пользователь с таким Email не найден");

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);

			var param = new Dictionary<string, string>
			{
				{ "token", token },
				{ "email", forgotPasswordDto.Email }
			};

			var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURI, param);

			var message = new EmailMessage(new string[] { user.Email }, "Восстановление пароля на портале 1С:Подпись",
				callback, null);

			await _emailSender.SendEmailAsync(message);

			return Ok();
		}

        [HttpPost("resetpassword")]
		public async Task<IActionResult> ResetPassword(
			[FromBody] ResetPasswordDto resetPasswordDto)
		{
			var errorResponse = new ResetPasswordResponseDto
			{
				Errors = new string[] { "Не удалось восстановить пароль" }
			};

			if (!ModelState.IsValid)
				return BadRequest(errorResponse);

			var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
			if (user == null)
				return BadRequest(errorResponse);

			var resetPassResult = await _userManager.ResetPasswordAsync(user,
				resetPasswordDto.Token, resetPasswordDto.Password);

			if (!resetPassResult.Succeeded)
			{
				var errors = resetPassResult.Errors.Select(e => e.Description);
				return BadRequest(new ResetPasswordResponseDto { Errors = errors });
			}

			await _userManager.SetLockoutEndDateAsync(user, null);

			return Ok(new ResetPasswordResponseDto { IsResetPasswordSuccessful = true });
		}

        public string GeneratePassword()
        {
            var options = _userManager.Options.Password;

            // int length = options.RequiredLength;
            int length = 15;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }
    }
}