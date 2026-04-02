using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.Models
{
    public class LoginViewModel 
    {
        [Required(ErrorMessage = "Въведи потребителско име")]
        [Display(Name = "Потребителско име")]
        public string Username 
        { get; set; } 

        [Required(ErrorMessage = "Въведи парола")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password
        { get; set; }

        [Required(ErrorMessage = "Избери роля")]
        [Display(Name = "Роля")]
        public string Role 
        { get; set; } 

        public bool RememberMe
        { get; set; } 
    }
}
