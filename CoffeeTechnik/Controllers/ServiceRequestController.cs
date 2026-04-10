using Microsoft.AspNetCore.Mvc;
using CoffeeTechnik.Data;
using CoffeeTechnik.Models;
using CoffeeTechnik.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTechnik.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceRequestController(ApplicationDbContext context)
        {
            _context = context;
        }
               
        [HttpGet]
        public IActionResult Index(string requestType, string searchString, int page = 1)
        {
            int pageSize = 5;

            var query = _context.ServiceRequests
                .Include(s => s.Machine)
                .ThenInclude(m => m.ObjectEntity)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(requestType))
            {
                query = query.Where(s => s.RequestType == requestType);
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(s =>
                    s.Description.Contains(searchString) ||
                    s.RequestType.Contains(searchString) ||
                    (s.Machine != null && s.Machine.Model.Contains(searchString)) ||
                    (s.Machine != null && s.Machine.ObjectEntity != null &&
                     s.Machine.ObjectEntity.Name.Contains(searchString)) ||
                    (s.Machine != null && s.Machine.ObjectEntity != null &&
                     s.Machine.ObjectEntity.City.Contains(searchString))
                );
            }

            int totalItems = query.Count();

            var items = query
                .OrderByDescending(s => s.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.CurrentFilter = requestType;
            ViewBag.SearchString = searchString;

            return View(items);
        }
               
        public IActionResult Details(int id)
        {
            var request = _context.ServiceRequests
                .Include(r => r.Machine)
                .ThenInclude(m => m.ObjectEntity)
                .FirstOrDefault(r => r.Id == id);

            if (request == null)
                return NotFound();

            return View(request);
        }

       
        public IActionResult CreateMontage() => View(new MontageViewModel());
        public IActionResult CreateDemontage() => View();
        public IActionResult CreateEmergency() => View();
        public IActionResult CreateMaintenance() => View();

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMontage(MontageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var obj = _context.Objects.FirstOrDefault(o => o.Name == model.ObjectName);

            if (obj == null)
            {
                obj = new ObjectEntity
                {
                    Name = model.ObjectName,
                    Bulstat = model.BULSTAT,
                    City = model.City,
                    Address = model.Address,
                    ContactPerson = model.ContactPerson,
                    PhoneNumber = model.Phone,
                    Type = "Монтаж",
                    Firma = "Неизвестна"
                };

                _context.Objects.Add(obj);
                _context.SaveChanges();
            }

            var machine = _context.Machines.FirstOrDefault(m =>
                m.Model == model.MachineModel &&
                m.ObjectEntityId == obj.Id);

            if (machine == null)
            {
                machine = new Machine
                {
                    Model = model.MachineModel,
                    SerialNumber = model.MachineConnection,
                    ObjectEntityId = obj.Id
                };

                _context.Machines.Add(machine);
                _context.SaveChanges();
            }

            _context.ServiceRequests.Add(new ServiceRequest
            {
                RequestType = "Монтаж",
                MachineId = machine.Id,
                Description = $"Монтаж на {machine.Model} за {obj.Name}",
                CreatedOn = DateTime.Now,
                Requester = model.Requester
            });

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката за монтаж е създадена!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDemontage(string objectName, string requester, string reason)
        {
            if (string.IsNullOrWhiteSpace(objectName) ||
                string.IsNullOrWhiteSpace(requester) ||
                string.IsNullOrWhiteSpace(reason))
            {
                TempData["ErrorMessage"] = "Попълни всички полета!";
                return RedirectToAction(nameof(CreateDemontage));
            }

            var obj = new ObjectEntity
            {
                Name = objectName,
                Type = "Демонтаж",
                Bulstat = "N/A",
                City = "N/A",
                Address = "N/A",
                ContactPerson = "N/A",
                PhoneNumber = "N/A",
                Firma = "Неизвестна"
            };

            _context.Objects.Add(obj);
            _context.SaveChanges();

            var machine = new Machine
            {
                Model = "N/A",
                SerialNumber = "N/A",
                ObjectEntityId = obj.Id
            };

            _context.Machines.Add(machine);
            _context.SaveChanges();

            _context.ServiceRequests.Add(new ServiceRequest
            {
                RequestType = "Демонтаж",
                MachineId = machine.Id,
                Description = reason,
                CreatedOn = DateTime.Now,
                Requester = requester
            });

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmergency(string objectName, string requester, string details)
        {
            if (string.IsNullOrWhiteSpace(objectName) ||
                string.IsNullOrWhiteSpace(requester) ||
                string.IsNullOrWhiteSpace(details))
            {
                TempData["ErrorMessage"] = "Попълни всички полета!";
                return RedirectToAction(nameof(CreateEmergency));
            }

            var obj = new ObjectEntity
            {
                Name = objectName,
                Type = "Авария",
                Bulstat = "N/A",
                City = "N/A",
                Address = "N/A",
                ContactPerson = "N/A",
                PhoneNumber = "N/A",
                Firma = "Неизвестна"
            };

            _context.Objects.Add(obj);
            _context.SaveChanges();

            var machine = new Machine
            {
                Model = "N/A",
                SerialNumber = "N/A",
                ObjectEntityId = obj.Id
            };

            _context.Machines.Add(machine);
            _context.SaveChanges();

            _context.ServiceRequests.Add(new ServiceRequest
            {
                RequestType = "Авария",
                MachineId = machine.Id,
                Description = details,
                CreatedOn = DateTime.Now,
                Requester = requester
            });

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMaintenance(string objectName, string requester, string details)
        {
            if (string.IsNullOrWhiteSpace(objectName) ||
                string.IsNullOrWhiteSpace(requester) ||
                string.IsNullOrWhiteSpace(details))
            {
                TempData["ErrorMessage"] = "Попълни всички полета!";
                return RedirectToAction(nameof(CreateMaintenance));
            }

            var obj = new ObjectEntity
            {
                Name = objectName,
                Type = "Профилактика",
                Bulstat = "N/A",
                City = "N/A",
                Address = "N/A",
                ContactPerson = "N/A",
                PhoneNumber = "N/A",
                Firma = "Неизвестна"
            };

            _context.Objects.Add(obj);
            _context.SaveChanges();

            var machine = new Machine
            {
                Model = "N/A",
                SerialNumber = "N/A",
                ObjectEntityId = obj.Id
            };

            _context.Machines.Add(machine);
            _context.SaveChanges();

            _context.ServiceRequests.Add(new ServiceRequest
            {
                RequestType = "Профилактика",
                MachineId = machine.Id,
                Description = details,
                CreatedOn = DateTime.Now,
                Requester = requester
            });

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}