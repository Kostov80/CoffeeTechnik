using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.Models.ViewModels
{
    public class ObjectViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [StringLength(100, ErrorMessage = "Името не може да надвишава 100 символа")]
        public string Name 
        { get; set; } = null!;

        [StringLength(100)]
        public string? Firma
        { get; set; }

        [StringLength(20)]
        public string? Bulstat
        { get; set; }

        [Required(ErrorMessage = "Типът е задължителен")]
        public string Type
        { get; set; } = null!;

        [StringLength(200)]
        public string? Address
        { get; set; }

        [Phone(ErrorMessage = "Невалиден телефонен номер")]
        public string? PhoneNumber 
        { get; set; }

        [StringLength(50)]
        public string? ContactPerson { get; set; }
    }
}