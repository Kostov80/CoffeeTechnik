using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.Models
{
    public class LoginViewModel// Модел за вход
    {
        [Required]
        [Display(Name = "Потребителско име")]//Показва името на полето в изгледа
        public string Username { get; set; }//Име на техника

        [Required]
        [DataType(DataType.Password)]//Парола
        [Display(Name = "Парола")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }//Запомня сесията
    }
}