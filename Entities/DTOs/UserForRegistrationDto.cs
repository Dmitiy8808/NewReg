using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage =  "Заполните Email")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage =  "Заполните Пароль")]
        public string Password { get; set; } = string.Empty;
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}