using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.Models
{
    public class ServiceStatus
    {
        public int Id 
        { get; set; }

        [Required]
        [StringLength(50)]
        public string Name 
        { get; set; } = null!;
    }
}