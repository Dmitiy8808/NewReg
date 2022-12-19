using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ResetPasswordResponseDto
    {
        public bool IsResetPasswordSuccessful { get; set; }
		public IEnumerable<string>? Errors { get; set; }
    }
}