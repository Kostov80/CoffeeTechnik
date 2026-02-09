using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.Models
{
    public class LoginViewModel// Модел за вход
    {
        [Required]
        public string Username { get; set; }//Име на техника

        [Required]
        [DataType(DataType.Password)]//Парола
        public string Password { get; set; }

        public bool RememberMe { get; set; }//Запомня сесията
    }
}