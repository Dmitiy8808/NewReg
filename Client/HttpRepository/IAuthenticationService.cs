using System.Net;
using Entities.DTOs;

namespace Client.HttpRepository
{
    public interface IAuthenticationService
    {
        Task<ResponseDto> RegisterUser(UserForRegistrationDto userForRegistrationDto);
        Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthenticationDto);
        Task Logout();
        Task<string> RefreshToken();
        Task<HttpStatusCode> ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}