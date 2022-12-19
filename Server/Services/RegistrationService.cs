using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Server.Data;
using Server.Services.EmailService;

namespace Server.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        public RegistrationService(UserManager<User> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task CreateIdentityUsersFromRequestAbonentList(RequestAbonentListDto requestAbonentCreateDtoList)
        {
            foreach (var listItem in requestAbonentCreateDtoList.AbonentList)
            {
                var user = await _userManager.FindByNameAsync(listItem.PersonEmail);
                if (user == null)
                {
                    var userCreationResponse = await CreateUser(listItem);
                    if (userCreationResponse.IsSuccessfulRegistration)
                    {
                        await SendPreRigistrationEmailConfirmation(listItem);
                    }
                }
                else
                {
                    await SendNewRequestEmailNotification(listItem);
                }
            }
        }

        public async Task SendNewRequestEmailNotification(RequestAbonentCreateDto requestAbonentCreateDto)
        {

			var callback = "http://localhost:5136/login";

			var message = new EmailMessage(new string[] { requestAbonentCreateDto.PersonEmail }, $"Для учетной записи {requestAbonentCreateDto.PersonEmail} На портале 1С:Подпись сформирована новая заявка",
				callback, null);

			await _emailSender.SendEmailAsync(message);

        }

        public async Task SendPreRigistrationEmailConfirmation(RequestAbonentCreateDto requestAbonentCreateDto)
        {
            var user = await _userManager.FindByEmailAsync(requestAbonentCreateDto.PersonEmail);

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);

			var param = new Dictionary<string, string>
			{
				{ "token", token },
				{ "email", requestAbonentCreateDto.PersonEmail }
			};

			var callback = QueryHelpers.AddQueryString("http://localhost:5136/setpassword", param);

			var message = new EmailMessage(new string[] { user.Email }, "Предсоздание аккаунта на портале 1С:Подпись",
				callback, null);

			await _emailSender.SendEmailAsync(message);

        }

        public async Task<ResponseDto> CreateUser(RequestAbonentCreateDto requestAbonentCreateDto)
        {
            var user = new User
            {
                UserName = requestAbonentCreateDto.PersonEmail,
                Email = requestAbonentCreateDto.PersonEmail
            };
            var password = GeneratePassword();

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return new ResponseDto {Errors = errors}; 
            }

            await _userManager.AddToRoleAsync(user, "User");

            return new ResponseDto {IsSuccessfulRegistration = true};
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