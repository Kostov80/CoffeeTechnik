using CoffeeTechnik.Data;
using CoffeeTechnik.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTechnik.Controllers
{
    public class ObjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ObjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()// Страница за обекти
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
        public IActionResult Create(ObjectEntity model)// Обработка на създаването на нов обект
        {
            if (!ModelState.IsValid)
            {
                ViewData["Types"] = GetObjectTypes();

                return View(model);
            }

            _context.Objects.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public IActionResult Edit(int id) // Страница за редактиране на обект
        {
            var obj = _context.Objects.FirstOrDefault(o => o.Id == id);
            if (obj == null)
                return NotFound();

            ViewData["Types"] = GetObjectTypes();
            return View(obj);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ObjectEntity model)// Обработка на редактирането на обект
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
            obj.PhoneNumber = model.PhoneNumber;
            obj.ContactPerson = model.ContactPerson;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        
        public IActionResult Demontage() // Страница за демонтаж
        {
            var demontageObjects = _context.Objects
                                           .Where(o => !o.Machines.Any())
                                           .ToList();

            return View(demontageObjects);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) // Изтриване на обект
        {
            var obj = _context.Objects.Find(id);

            if (obj == null)
                return NotFound();

            if (obj.Machines.Any())
            {
                TempData["Error"] = "Обектът има машини и не може да се изтрие!";
                return RedirectToAction("Index");
            }

            _context.Objects.Remove(obj);
            _context.SaveChanges();

            TempData["Success"] = "Обектът беше успешно изтрит.";
            return RedirectToAction("Index");
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
