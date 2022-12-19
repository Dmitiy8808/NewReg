using System;
using System.Collections.Generic;
using System.Linq;
using Client.HttpRepository;
using Entities.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;

namespace Reg.Client.Pages.Authentication
{
    public partial class ResetPassword
    {
        private readonly ResetPasswordDto _resetPassDto = new ResetPasswordDto();
		private bool _showError;
		private bool _showSuccess;
		private IEnumerable<string>? _errors;

		[Inject]
		public IAuthenticationService? AuthService { get; set; }
		[Inject]
		public NavigationManager? NavigationManager { get; set; }
         private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        private void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }

		protected override void OnInitialized()
		{
			var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
			var queryStrings = QueryHelpers.ParseQuery(uri.Query);
			if (queryStrings.TryGetValue("email", out var email) && 
				queryStrings.TryGetValue("token", out var token))
			{
				_resetPassDto.Email = email;
				_resetPassDto.Token = token;
			}
			else
			{
				NavigationManager.NavigateTo("/");
			}
		}

		private async Task Submit()
		{
			_showSuccess = _showError = false;
			var result = await AuthService.ResetPassword(_resetPassDto);
			if (result.IsResetPasswordSuccessful)
				_showSuccess = true;
			else
			{
				_errors = result.Errors;
				_showError = true;
			}
		}

        void Authorize()
        {
            NavigationManager.NavigateTo("/login");
        }
    }
}