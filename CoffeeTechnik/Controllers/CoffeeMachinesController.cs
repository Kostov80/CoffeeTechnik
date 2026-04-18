using CoffeeTechnik.Models.ViewModels;
using CoffeeTechnik.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoffeeTechnik.Controllers
{
    public class CoffeeMachinesController : Controller
    {
        private readonly ICoffeeService _coffeeService;

        public CoffeeMachinesController(ICoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

        public async Task<IActionResult> Index()
        {
            var machines = await _coffeeService.GetAllAsync();
            return View(machines);
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var machine = await _coffeeService.GetByIdAsync(id);

            if (machine == null)
                return NotFound();

            return View(machine);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoffeeMachineViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _coffeeService.CreateAsync(model);

            TempData["SuccessMessage"] = "Машината беше добавена успешно!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var machine = await _coffeeService.GetByIdAsync(id);

            if (machine == null)
                return NotFound();

            return View(machine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CoffeeMachineViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _coffeeService.UpdateAsync(model);

            TempData["SuccessMessage"] = "Машината беше редактирана успешно!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var machine = await _coffeeService.GetByIdAsync(id);

            if (machine == null)
                return NotFound();

            await _coffeeService.DeleteAsync(id);

            TempData["SuccessMessage"] = "Машината беше изтрита успешно!";

            return RedirectToAction(nameof(Index));
        }
    }
}