using CoffeeTechnik.Data;
using CoffeeTechnik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeTechnik.Controllers
{
    public class ObjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ObjectsController(ApplicationDbContext context)
        {
            _context = context;
        }
                
        public async Task<IActionResult> Index()
        {
            var objects = await _context.Objects
                                        .Include(o => o.Machines)
                                        .OrderByDescending(o => o.Id)
                                        .ToListAsync();
            return View(objects);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Types"] = GetObjectTypes();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Firma,Bulstat,Type,Address,City,PhoneNumber,ContactPerson")] ObjectEntity model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Types"] = GetObjectTypes();
                return View(model);
            }

            _context.Objects.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Обектът беше добавен успешно.";
            return RedirectToAction(nameof(Index));
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var obj = await _context.Objects.FindAsync(id);
            if (obj == null)
                return NotFound();

            ViewData["Types"] = GetObjectTypes();
            return View(obj);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Firma,Bulstat,Type,Address,City,PhoneNumber,ContactPerson")] ObjectEntity model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Types"] = GetObjectTypes();
                return View(model);
            }

            var obj = await _context.Objects.FindAsync(model.Id);
            if (obj == null)
                return NotFound();

            obj.Name = model.Name;
            obj.Firma = model.Firma;
            obj.Bulstat = model.Bulstat;
            obj.Type = model.Type;
            obj.Address = model.Address;
            obj.City = model.City;
            obj.PhoneNumber = model.PhoneNumber;
            obj.ContactPerson = model.ContactPerson;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Обектът беше редактиран успешно.";
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _context.Objects.Include(o => o.Machines)
                                            .FirstOrDefaultAsync(o => o.Id == id);

            if (obj == null)
                return NotFound();

            if (obj.Machines != null && obj.Machines.Any())
            {
                TempData["Error"] = "Обектът не може да бъде изтрит, защото има свързани машини.";
                return RedirectToAction(nameof(Index));
            }

            _context.Objects.Remove(obj);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Обектът беше успешно изтрит.";
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Demontage()
        {
            var demontageObjects = await _context.Objects
                                                 .Where(o => !o.Machines.Any())
                                                 .ToListAsync();
            return View(demontageObjects);
        }

        private List<SelectListItem> GetObjectTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Хотел", Text = "Хотел" },
                new SelectListItem { Value = "Заведение", Text = "Заведение" },
                new SelectListItem { Value = "Магазин", Text = "Магазин" },
                new SelectListItem { Value = "Офис", Text = "Офис" }
            };
        }
    }
}