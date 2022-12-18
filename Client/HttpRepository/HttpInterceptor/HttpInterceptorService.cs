
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor;

namespace Client.HttpRepository.HttpInterceptor
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navManager;
        private readonly RefreshTokenService _refreshTokenService;
        public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager,
            RefreshTokenService refreshTokenService)
        {
            _interceptor = interceptor;
            _navManager = navManager;
            _refreshTokenService = refreshTokenService;
        }

        public void RegisterEvent() => _interceptor.AfterSend += HandleResponse;
        public void RegisterBeforeSendEvent() =>
			_interceptor.BeforeSendAsync += InterceptBeforeSendAsync;
        public void DisposeEvent()
		{
			_interceptor.AfterSend -= HandleResponse;
			_interceptor.BeforeSendAsync -= InterceptBeforeSendAsync;
		}

        private async Task InterceptBeforeSendAsync(object sender, 
			HttpClientInterceptorEventArgs e)
		{
			var absolutePath = e.Request.RequestUri.AbsolutePath;

			if (!absolutePath.Contains("token") && !absolutePath.Contains("account"))
			{
				var token = await _refreshTokenService.TryRefreshToken();
				if (!string.IsNullOrEmpty(token))
				{
					e.Request.Headers.Authorization = 
						new AuthenticationHeaderValue("bearer", token);
				}
			}
		}

        private void HandleResponse(object? sender, HttpClientInterceptorEventArgs e)
        {
            if(e.Response == null)
            {
                _navManager.NavigateTo("/error");
                throw new HttpResponseException("Сервер недоступен");
            }

            var message = "";

            if(!e.Response.IsSuccessStatusCode)
            {
                switch (e.Response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        _navManager.NavigateTo("/404");
                        message = "Страница не найдена";
                        break;
                    case HttpStatusCode.BadRequest:
                        message = "Неверный запрос. Попробуйте заново";
                        break;
                    case HttpStatusCode.Unauthorized:
                        _navManager.NavigateTo("/unauthorized");
                        message = "Доступ запрещен";
                        break;
                    default:
                        _navManager.NavigateTo("/error");
                        message = "Что-то пошло не так";
                        break;
                }

                throw new HttpResponseException(message);
            }
        }
    }
}