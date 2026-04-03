using CoffeeTechnik.Data;
using CoffeeTechnik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoffeeTechnik.Controllers
{
    public class ObjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ObjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            
            var objects = _context.Objects
                          .Include(o => o.Machines)
                          .OrderByDescending(o => o.Id)
                          .ToList();

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
        public IActionResult Create(ObjectEntity model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Types"] = GetObjectTypes();
                return View(model);
            }

            _context.Objects.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "Обектът беше добавен успешно.";
            return RedirectToAction(nameof(Index));
        }

        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var obj = _context.Objects.FirstOrDefault(o => o.Id == id);
            if (obj == null)
                return NotFound();

            ViewData["Types"] = GetObjectTypes();
            return View(obj);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ObjectEntity model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Types"] = GetObjectTypes();
                return View(model);
            }

            var obj = _context.Objects.FirstOrDefault(o => o.Id == model.Id);
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

            _context.SaveChanges();

            TempData["Success"] = "Обектът беше редактиран успешно.";
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = _context.Objects.Include(o => o.Machines)
                                      .FirstOrDefault(o => o.Id == id);

            if (obj == null)
                return NotFound();

            if (obj.Machines != null && obj.Machines.Any())
            {
                TempData["Error"] = "Обектът не може да бъде изтрит, защото има свързани машини.";
                return RedirectToAction(nameof(Index));
            }

            _context.Objects.Remove(obj);
            _context.SaveChanges();

            TempData["Success"] = "Обектът беше успешно изтрит.";
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Demontage()
        {
            var demontageObjects = _context.Objects
                                           .Where(o => !o.Machines.Any())
                                           .ToList();

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