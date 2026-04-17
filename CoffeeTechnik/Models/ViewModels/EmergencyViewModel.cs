using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.ViewModels
{
    public class EmergencyViewModel
    {
        [Required(ErrorMessage = "Полето 'Обект' е задължително.")]
        [Display(Name = "Обект")]
        public string ObjectName
        { get; set; }

        [Required(ErrorMessage = "Полето 'Описание на аварията' е задължително.")]
        [Display(Name = "Описание на аварията")]
        public string Details 
        { get; set; }

        [Required(ErrorMessage = "Полето 'Подал заявката' е задължително.")]
        [Display(Name = "Подал заявката")]
        public string Requester
        { get; set; }

        
        public int? MachineId
        { get; set; }
    }
}