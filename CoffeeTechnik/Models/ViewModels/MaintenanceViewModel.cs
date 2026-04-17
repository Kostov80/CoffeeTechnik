using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.ViewModels
{
    public class MaintenanceViewModel
    {
        [Required(ErrorMessage = "Полето 'Обект' е задължително.")]
        [Display(Name = "Обект")]
        public string ObjectName
        { get; set; }

        [Required(ErrorMessage = "Полето 'Подал заявката' е задължително.")]
        [Display(Name = "Подал заявката")]
        public string RequestFrom 
        { get; set; }

        
        public int? MachineId 
        { get; set; }
    }
}