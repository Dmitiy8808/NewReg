using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RefreshTokenDto
    {
        public string Token { get; set; }
		public string RefreshToken { get; set; }
    }
}