using System;

namespace CoffeeTechnik.Models.ViewModels
{
    public class CoffeeMachineViewModel
    {
        public int Id 
        { get; set; }
        public string Model
        { get; set; } = null!;
        public string SerialNumber 
        { get; set; } = null!;
        public DateTime? InstallationDate 
        { get; set; }
        public int ObjectEntityId
        { get; set; }
    }
}