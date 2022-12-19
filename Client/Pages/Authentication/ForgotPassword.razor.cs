using System.Net;
using Client.HttpRepository;
using Entities.DTOs;
using Microsoft.AspNetCore.Components;

namespace Reg.Client.Pages.Authentication
{
    public partial class ForgotPassword
    {
        private ForgotPasswordDto _forgotPassDto = new ForgotPasswordDto();
		private bool _showSuccess;
		private bool _showError;


		[Inject]
		public IAuthenticationService? AuthService { get; set; }

		private async Task Submit()
		{
			_showSuccess = _showError = false;

			var result = await AuthService.ForgotPassword(_forgotPassDto);
			if (result == HttpStatusCode.OK)
				_showSuccess = true;
			else
				_showError = true;
		}
    }
}