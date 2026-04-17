using CoffeeTechnik.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeTechnik.Services.Interfaces
{
    public interface ICoffeeService
    {
       
        Task<IEnumerable<CoffeeMachineViewModel>> GetAllAsync();

        Task<CoffeeMachineViewModel?> GetByIdAsync(int id);
        
        Task CreateAsync(CoffeeMachineViewModel model);

        Task UpdateAsync(CoffeeMachineViewModel model);

        Task DeleteAsync(int id);
    }
}