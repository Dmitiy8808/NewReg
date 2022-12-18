using System.Text.RegularExpressions;
using Client.HttpRepository;
using Entities.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Reg.Client.Pages.Authentication
{
    public partial class Registration
    {
        MudForm form;
        bool success;
        string[] errors = { };
        MudTextField<string> pwField1;
        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
        private UserForRegistrationDto _userForRegistrationDto = new UserForRegistrationDto();
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAuthenticationService AauthenticationService { get; set; }
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public async Task Register()
        {
            await form.Validate();
            if (success)
            {
                ShowRegistrationErrors = false;
                var result = await AauthenticationService.RegisterUser(_userForRegistrationDto);
                if (!result.IsSuccessfulRegistration)
                {
                    Errors = result.Errors;
                    ShowRegistrationErrors = true;
                }
                else
                {
                    NavigationManager.NavigateTo("/login");
                }
            }
            
        }     

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

         private IEnumerable<string> PasswordStrength(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                yield return "Заполните пароль!";
                yield break;
            }
            if (pw.Length < 8)
                yield return "Пароль должен быть не менее 8 символов";
            if (!Regex.IsMatch(pw, @"[A-Z]"))
                yield return "Пароль должен содержать хотябы одну заглавную букву";
            if (!Regex.IsMatch(pw, @"[a-z]"))
                yield return "Пароль должен содержать хотябы одну прописную букву";
            if (!Regex.IsMatch(pw, @"[0-9]"))
                yield return "Пароль должен содержать хотябы одну прописную цифру";
        }

        private string PasswordMatch(string arg)
        {
            if (pwField1.Value != arg)
                return "Пароли не совпадают";
            return null;
        }
    }
}