using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

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

        [Required]
        [Display(Name = "Адрес")]
        public string Address 
        { get; set; } = null!;

        [Display(Name = "Телефон")]
        public string? PhoneNumber 
        { get; set; }

        
        public ICollection<Machine> Machines // /Един обект има много машини
        { get; set; } = new List<Machine>();
    }
}
