using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.AuthProviders
{
    public class TestAuthStateProvider : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(1500);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Dmitriy Stupin"),
                new Claim(ClaimTypes.Role, "Administrator"),
            };
            var anonymous = new ClaimsIdentity(claims, "testAuthType");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
        }
    }
}