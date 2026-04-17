using CoffeeTechnik.Models.ViewModels;
using CoffeeTechnik.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoffeeTechnik.Controllers
{
    [Authorize(Roles = "Sales,Technician")] 
    public class CoffeeController : Controller
    {
        private readonly ICoffeeService _coffeeService;


        public CoffeeController(ICoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

        
        public async Task<IActionResult> Index()
        {
            try
            {
                var coffees = await _coffeeService.GetAllAsync();
                return View(coffees);
            }
            catch (Exception ex)
            {
                
                 return View("Error", ex);
            }
        }

        
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var coffee = await _coffeeService.GetByIdAsync(id);
                if (coffee == null)
                {
                    return NotFound(); 
                }

                return View(coffee);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        
        [Authorize(Roles = "Sales")]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Sales")]
        public async Task<IActionResult> Create(CoffeeMachineViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _coffeeService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Възникна грешка при създаване на машината.");
                
                return View(model);
            }
        }

        
        [Authorize(Roles = "Sales")]
        public async Task<IActionResult> Edit(int id)
        {
            var coffee = await _coffeeService.GetByIdAsync(id);
            if (coffee == null)
            {
                return NotFound();
            }
            return View(coffee);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Sales")]
        public async Task<IActionResult> Edit(int id, CoffeeMachineViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _coffeeService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Възникна грешка при редактиране на машината.");
                
                return View(model);
            }
        }

        
        [Authorize(Roles = "Sales")]
        public async Task<IActionResult> Delete(int id)
        {
            var coffee = await _coffeeService.GetByIdAsync(id);
            if (coffee == null)
            {
                return NotFound();
            }
            return View(coffee);
        }


        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Sales")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _coffeeService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                

                return View("Error", ex);
            }
        }
    }
}