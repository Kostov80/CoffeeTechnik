using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.ViewModels
{
    public class InventoryItemViewModel
    {
        [Required(ErrorMessage = "Името е задължително")]
        public string Name { get; set; }

        [Range(0, 10000, ErrorMessage = "Невалидно количество")]
        public int Quantity { get; set; }

        public string Description { get; set; }
    }
}