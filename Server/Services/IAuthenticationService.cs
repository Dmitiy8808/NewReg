using System.Security.Claims;
using Server.Data;

namespace Server.Services
{
    public interface IAuthenticationService
    {
        Task<string> GetToken(User user);
        string GenerateRefreshToken();
		ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}