using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeTechnik.Models
{
    public class CoffeeMachine
    {
        public int Id 
        { get; set; }

        [Required]
        [StringLength(100)]
        public string Model
        { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string SerialNumber
        { get; set; } = null!;

        [Display(Name = "Дата на инсталация")]
        [DataType(DataType.Date)]
        public DateTime? InstallationDate
        { get; set; }

        [Required]
        [Display(Name = "Обект")]
        public int ObjectEntityId
        { get; set; }

        [ForeignKey(nameof(ObjectEntityId))]
        public ObjectEntity ObjectEntity
        { get; set; } = null!;

        public ICollection<ServiceRequest> ServiceRequests
        { get; set; } = new List<ServiceRequest>();
    }
}