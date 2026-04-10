using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeTechnik.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }

      
        [Required(ErrorMessage = "Типът на заявката е задължителен")]
        [StringLength(50)]
        [Display(Name = "Тип заявка")]
        public string RequestType
        { get; set; } = null!;

        [Required(ErrorMessage = "Подателят е задължителен")]
        [StringLength(100)]
        [Display(Name = "Подал заявката")]
        public string Requester 
        { get; set; } = null!;

        [Required(ErrorMessage = "Описанието е задължително")]
        [StringLength(500, MinimumLength = 5,
            ErrorMessage = "Описанието трябва да е между 5 и 500 символа")]
        [Display(Name = "Описание")]
        public string Description
        { get; set; } = null!;

        [Display(Name = "Дата на създаване")]
        public DateTime CreatedOn
        { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Статус")]
        [StringLength(50)]
        public string Status { get; set; } = "Нова";


        [Required(ErrorMessage = "Избери машина")]
        [Display(Name = "Машина")]
        public int MachineId 
        { get; set; }

        [ForeignKey("MachineId")]
        public Machine Machine 
        { get; set; } = null!;

        
        [StringLength(500)]
        [Display(Name = "Описание монтаж")]
        public string? MontajOpisanie 
        { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата монтаж")]
        public DateTime? MontajDate 
        { get; set; }

        [StringLength(500)]
        [Display(Name = "Описание профилактика")]
        public string? ProfilaktikaOpisanie 
        { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата профилактика")]
        public DateTime? ProfilaktikaDate 
        { get; set; }
                
        [StringLength(500)]
        [Display(Name = "Описание авария")]
        public string? AvariaOpisanie 
        { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата авария")]
        public DateTime? AvariaDate 
        { get; set; }
    }
}