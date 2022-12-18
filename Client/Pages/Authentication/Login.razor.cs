using Entities.DTOs;
using Microsoft.AspNetCore.Components;
using Client.HttpRepository;
using MudBlazor;

namespace Reg.Client.Pages.Authentication
{
    public partial class Login
    {
        private UserForAuthenticationDto _userForAuthentication = 
			new UserForAuthenticationDto();

		[Inject]
		public IAuthenticationService AuthenticationService { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		public bool ShowAuthError { get; set; }
		public string Error { get; set; }
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
        private InputType _passwordInput = InputType.Password;
        private bool _passwordVisibility;

		public async Task ExecuteLogin()
		{
			ShowAuthError = false;

			var result = await AuthenticationService.Login(_userForAuthentication);
			if (!result.IsAuthSuccessful)
			{
				Error = result.ErrorMessage;
				ShowAuthError = true;
			}
			else
			{
				NavigationManager.NavigateTo("/");
			}
		}

        void TogglePasswordVisibility()
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
    }
}