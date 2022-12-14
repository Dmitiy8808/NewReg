using Microsoft.AspNetCore.Identity;

namespace Server.Data
{
    public class User : IdentityUser
    {
        public string? RefreshToken { get; set; }
		public DateTime RefreshTokenExpiryTime { get; set; }
    }
}