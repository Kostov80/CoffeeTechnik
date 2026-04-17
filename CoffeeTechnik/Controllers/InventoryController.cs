using Microsoft.AspNetCore.Mvc;
using CoffeeTechnik.Data;
using CoffeeTechnik.Models;
using CoffeeTechnik.ViewModels;
using System.Linq;

namespace CoffeeTechnik.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult Index()
        {
            var items = _context.InventoryItems.ToList();
            return View(items);
        }

       
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InventoryItemViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var item = new InventoryItem
            {
                Name = model.Name,
                Quantity = model.Quantity,
                Description = model.Description
            };

            _context.InventoryItems.Add(item);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Артикулът е добавен успешно!";
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var item = _context.InventoryItems.Find(id);

            if (item == null)
            {
                TempData["ErrorMessage"] = "Артикулът не е намерен!";
                return RedirectToAction(nameof(Index));
            }

            _context.InventoryItems.Remove(item);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Артикулът е изтрит успешно!";
            return RedirectToAction(nameof(Index));
        }
    }
}