using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeTechnik.Models
{
    public class Machine
    {
        public int Id 
        { get; set; }



        [Required(ErrorMessage = "Моделът е задължителен.")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Моделът трябва да бъде между 2 и 100 символа.")]
        [Display(Name = "Модел")]
        public string Model
        { get; set; } = null!;


        [Required(ErrorMessage = "Серийният номер е задължителен.")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Серийният номер трябва да бъде между 3 и 50 символа.")]
        [Display(Name = "Сериен номер")]
        public string SerialNumber 
        { get; set; } = null!;



        [Display(Name = "Дата на инсталация")]
        [DataType(DataType.Date)]
        public DateTime? InstallationDate 
        { get; set; }


        [Required(ErrorMessage = "Трябва да изберете обект.")]
        [Display(Name = "Обект")]
        public int ObjectEntityId
        { get; set; }


        [ForeignKey(nameof(ObjectEntityId))]
        public ObjectEntity ObjectEntity
        { get; set; } = null!;


        // Една машина има много заявки
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}
