using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Server.Data
{
    public class User : IdentityUser
    {
        public string RefreshToken { get; set; }
		public DateTime RefreshTokenExpiryTime { get; set; }
    }
}