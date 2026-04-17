using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Въведете бройка")]
        [Range(1, int.MaxValue, ErrorMessage = "Бройката трябва да е поне 1")]
        public int Quantity { get; set; }

        public string? Description { get; set; }
    }
}