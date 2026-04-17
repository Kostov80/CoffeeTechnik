using CoffeeTechnik.Models;

namespace CoffeeTechnik.ViewModels
{
    public class ServiceRequestListResult
    {
        public List<ServiceRequest> Items 
        { get; set; } = new();

        public int CurrentPage 
        { get; set; }

        public int TotalPages 
        { get; set; }
    }
}