using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeTechnik.Models
{
    public class ServiceRequest
    {
        public int Id 
        { get; set; }


        [Required]
        [Display(Name = "Тип заявка")]
        public string RequestType
        { get; set; } = null!;


        [Required]
        [Display(Name = "Подал заявката")]
        public string Requester
        { get; set; } = null!;


        [Required]
        [Display(Name = "Описание")]
        [StringLength(500, ErrorMessage = "Описанието може да бъде до 500 символа.")]
        public string Description
        { get; set; } = null!;


        [Display(Name = "Дата на създаване")]
        public DateTime CreatedOn
        { get; set; } = DateTime.Now;


        [Display(Name = "Статус")]
        public string Status
        { get; set; } = "Нова";


        
        [Display(Name = "Машина")]
        public int MachineId 
        { get; set; }


        [ForeignKey("MachineId")]
        public Machine Machine
        { get; set; } = null!;

        public DateTime CreatedAt 
        { get; internal set; }

        public string? MontajOpisanie
        { get; set; }

        public DateTime? MontajDate
        { get; set; }


        public string? ProfilaktikaOpisanie 
        { get; set; }

        public DateTime? ProfilaktikaDate 
        { get; set; }


        public string? AvariaOpisanie 
        { get; set; }

        public DateTime? AvariaDate 
        { get; set; }

    }
}
