using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeTechnik.Models
{
    public class ObjectEntity
    {
        public int Id
        { get; set; }

        [Required]
        [Display(Name = "Име на обекта")]
        public string Name 
        { get; set; } = null!;

        [Display(Name = "Град")]
        public string? City 
        { get; set; }

        [Required]
        [Display(Name = "Фирма")]
        public string Firma 
        { get; set; } = null!;

        [Required]
        [Display(Name = "Булстат")]
        public string Bulstat
        { get; set; } = null!;

        [Required]
        [Display(Name = "Тип на обекта")]
        public string Type
        { get; set; } = null!;

        [Required]
        [Display(Name = "Адрес")]
        public string Address 
        { get; set; } = null!;

        [Display(Name = "Телефон")]
        public string? PhoneNumber
        { get; set; }

        [Display(Name = "Контактно лице")]
        public string? ContactPerson
        { get; set; }

        
        public ICollection<Machine> Machines
        { get; set; } = new List<Machine>();

        
        [NotMapped] 
        [Display(Name = "Брой машини")]
        public int MachineCount => Machines?.Count ?? 0;
    }
}