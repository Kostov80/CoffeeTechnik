using CoffeeTechnik.Data;
using CoffeeTechnik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeTechnik.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public async Task<IActionResult> Parts(string searchString)
        {
            var parts = _context.InventoryItems.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                parts = parts.Where(p => p.Name.Contains(searchString));
            }

            return View(await parts.ToListAsync());
        }

        [HttpGet]
        public IActionResult CreatePart()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePart(InventoryItem model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.InventoryItems.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Parts));
        }

        [HttpGet]
        public async Task<IActionResult> EditPart(int id)
        {
            var item = await _context.InventoryItems.FindAsync(id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPart(InventoryItem model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var item = await _context.InventoryItems.FindAsync(model.Id);

            if (item == null)
                return NotFound();

            item.Name = model.Name;
            item.Quantity = model.Quantity;
            item.Description = model.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Parts));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePart(int id)
        {
            var item = await _context.InventoryItems.FindAsync(id);

            if (item == null)
                return NotFound();

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Parts));
        }
    }
}