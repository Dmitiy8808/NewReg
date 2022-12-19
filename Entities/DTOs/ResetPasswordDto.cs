using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Веедите пароль")]
		public string? Password { get; set; }
		[Compare(nameof(Password), 
			ErrorMessage = "Пароли не совпадают")]
		public string? ConfirmPassword { get; set; }
		public string? Email { get; set; }
		public string? Token { get; set; }
    }
}