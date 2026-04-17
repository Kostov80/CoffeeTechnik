using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Въведи име")]
        [RegularExpression(@"^[A-Za-zА-Яа-я]+$", ErrorMessage = "Името може да съдържа само букви")]
        public string FirstName
        { get; set; } = null!;

        [Required(ErrorMessage = "Въведи фамилия")]
        [RegularExpression(@"^[A-Za-zА-Яа-я]+$", ErrorMessage = "Фамилията може да съдържа само букви")]
        public string LastName
        { get; set; } = null!;

        [Required(ErrorMessage = "Въведи телефон")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Телефонът може да съдържа само цифри")]
        public string PhoneNumber
        { get; set; } = null!;

        [Required(ErrorMessage = "Въведи потребителско име")]
        public string Username 
        { get; set; } = null!;

        [Required(ErrorMessage = "Въведи парола")]
        [MinLength(5, ErrorMessage = "Паролата трябва да е минимум 5 символа")]
        [DataType(DataType.Password)]
        public string Password
        { get; set; } = null!;

        [Required(ErrorMessage = "Избери роля")]
        public string Role
        { get; set; } = null!;
    }
}