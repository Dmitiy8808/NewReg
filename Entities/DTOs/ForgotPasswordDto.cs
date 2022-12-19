using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class ForgotPasswordDto
    {
        [Required]
		[EmailAddress]
		public string? Email { get; set; }
		public string? ClientURI { get; set; }
    }
}