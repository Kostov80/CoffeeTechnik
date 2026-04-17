using CoffeeTechnik.Models;
using CoffeeTechnik.ViewModels;

namespace CoffeeTechnik.Services.Interfaces
{
    public interface IServiceRequestService
    {
        ServiceRequestListResult GetAll(string requestType, string searchString, int page);

        ServiceRequest GetById(int id);

        void CreateMontage(MontageViewModel model);

        void CreateDemontage(string objectName, string requester, string reason);

        void CreateEmergency(string objectName, string requester, string details);

        void CreateMaintenance(string objectName, string requester, string details);

        bool Delete(int id);
    }

  
}