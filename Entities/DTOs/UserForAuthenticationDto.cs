using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Введите Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите пароль.")]
        public string Password { get; set; }
    }
}