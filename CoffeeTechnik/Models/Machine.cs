using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeTechnik.Models
{
    public class Machine
    {
        public int Id 
        { get; set; }



        [Required]
        [Display(Name = "Модел")]
        public string Model
        { get; set; } = null!;



        [Required]
        [Display(Name = "Сериен номер")]
        public string SerialNumber 
        { get; set; } = null!;

        [Display(Name = "Дата на инсталация")]
        public DateTime? InstallationDate
        { get; set; }


        
        [Display(Name = "Обект")]
        public int ObjectEntityId
        { get; set; }


        [ForeignKey("ObjectEntityId")]
        public ObjectEntity ObjectEntity 
        { get; set; } = null!;

        

        public ICollection<ServiceRequest> ServiceRequests // Една машина има много заявки
        { get; set; } = new List<ServiceRequest>();
    }
}
