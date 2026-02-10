using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.Models
{
    public class LoginViewModel // Модел за вход
    {
        [Required(ErrorMessage = "Въведи потребителско име")]
        [Display(Name = "Потребителско име")]
        public string Username 
        { get; set; } // Потребителско име

        [Required(ErrorMessage = "Въведи парола")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password
        { get; set; }

        [Required(ErrorMessage = "Избери роля")]
        [Display(Name = "Роля")]
        public string Role 
        { get; set; } // Technician / Sales

        public bool RememberMe
        { get; set; } // Запомня сесията
    }
}
