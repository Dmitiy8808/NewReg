using System.Net.Http.Json;
using System.Text.Json;
using Entities.DTOs;
using Blazored.LocalStorage;
using Client.AuthProviders;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Components;

namespace Client.HttpRepository
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly JsonSerializerOptions _options = 
            new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
		private readonly NavigationManager _navManager;

        public AuthenticationService(HttpClient client,
			AuthenticationStateProvider authStateProvider,
			ILocalStorageService localStorage,
			NavigationManager navManager)
        {
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _client = client;
			_navManager = navManager;
            
        }
        public async Task<ResponseDto> RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            var response = await _client.PostAsJsonAsync("account/register", userForRegistrationDto);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<ResponseDto>(content, _options);

                return result;
            }

            return new ResponseDto { IsSuccessfulRegistration = true};
        }

		// public async Task<ResponseDto> RegisterUserByEmail(UserForRegistrationListDto userForRegistrationListDto)
        // {
        //     var response = await _client.PostAsJsonAsync("account/registerEmail", userForRegistrationListDto);
        //     if (!response.IsSuccessStatusCode)
        //     {
        //         var content = await response.Content.ReadAsStringAsync();

        //         var result = JsonSerializer.Deserialize<ResponseDto>(content, _options);

        //         return result;
        //     }

        //     return new ResponseDto { IsSuccessfulRegistration = true};
        // }

        public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
		{
			var response = await _client.PostAsJsonAsync("account/login",
				userForAuthentication);
			var content = await response.Content.ReadAsStringAsync();

			var result = JsonSerializer.Deserialize<AuthResponseDto>(content, _options);
			
			if (!response.IsSuccessStatusCode)
				return result;

			await _localStorage.SetItemAsync("authToken", result.Token);
			await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

			((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(
				result.Token);

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				"bearer", result.Token);

			return new AuthResponseDto { IsAuthSuccessful = true };
		}

		public async Task Logout()
		{
			await _localStorage.RemoveItemAsync("authToken");
			await _localStorage.RemoveItemAsync("refreshToken");
			
			((AuthStateProvider)_authStateProvider).NotifyUserLogout();

			_client.DefaultRequestHeaders.Authorization = null;
		}

        public async Task<string> RefreshToken()
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");
			var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

			var response = await _client.PostAsJsonAsync("token/refresh",
				new RefreshTokenDto
				{
					Token = token,
					RefreshToken = refreshToken
				});

			var content = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<AuthResponseDto>(content, _options);

			await _localStorage.SetItemAsync("authToken", result.Token);
			await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
				("bearer", result.Token);

			return result.Token;
		}

        public async Task<HttpStatusCode> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
		{
			forgotPasswordDto.ClientURI =
				Path.Combine(_navManager.BaseUri, "setpassword");

			var result = await _client.PostAsJsonAsync("account/forgotpassword",
				forgotPasswordDto);

			return result.StatusCode;
		}

        public async Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordDto resetPasswordDto)
		{
			var resetResult = await _client.PostAsJsonAsync("account/resetpassword",
				resetPasswordDto);

			var resetContent = await resetResult.Content.ReadAsStringAsync();

			var result = JsonSerializer.Deserialize<ResetPasswordResponseDto>(resetContent,
				_options);

			return result;
		}
    }
}