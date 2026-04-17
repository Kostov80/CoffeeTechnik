using CoffeeTechnik.Data;
using CoffeeTechnik.Models;
using CoffeeTechnik.Models.ViewModels;
using CoffeeTechnik.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeTechnik.Services.Implementations
{
    public class CoffeeService : ICoffeeService
    {
        private readonly ApplicationDbContext _context;

        public CoffeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IEnumerable<CoffeeMachineViewModel>> GetAllAsync()
        {
            return await _context.CoffeeMachines
                .AsNoTracking() 
                .OrderByDescending(c => c.Id) 
                .Select(c => new CoffeeMachineViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    SerialNumber = c.SerialNumber,
                    InstallationDate = c.InstallationDate,
                    ObjectEntityId = c.ObjectEntityId
                })
                .ToListAsync();
        }
        
        public async Task<CoffeeMachineViewModel?> GetByIdAsync(int id)
        {
            var coffee = await _context.CoffeeMachines
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coffee == null)
                return null;

            return new CoffeeMachineViewModel
            {
                Id = coffee.Id,
                Model = coffee.Model,
                SerialNumber = coffee.SerialNumber,
                InstallationDate = coffee.InstallationDate,
                ObjectEntityId = coffee.ObjectEntityId
            };
        }
                
        public async Task CreateAsync(CoffeeMachineViewModel model)
        {
            if (model == null) return;

            var coffee = new CoffeeMachine
            {
                Model = model.Model,
                SerialNumber = model.SerialNumber,
                InstallationDate = model.InstallationDate,
                ObjectEntityId = model.ObjectEntityId
            };

            await _context.CoffeeMachines.AddAsync(coffee);
            await _context.SaveChangesAsync();
        }
                
        public async Task UpdateAsync(CoffeeMachineViewModel model)
        {
            if (model == null) return;

            var coffee = await _context.CoffeeMachines.FindAsync(model.Id);
            if (coffee == null) return;

            coffee.Model = model.Model;
            coffee.SerialNumber = model.SerialNumber;
            coffee.InstallationDate = model.InstallationDate;
            coffee.ObjectEntityId = model.ObjectEntityId;

            await _context.SaveChangesAsync();
        }
                
        public async Task DeleteAsync(int id)
        {
            var coffee = await _context.CoffeeMachines.FindAsync(id);
            if (coffee == null) return;

            _context.CoffeeMachines.Remove(coffee);
            await _context.SaveChangesAsync();
        }
    }
}