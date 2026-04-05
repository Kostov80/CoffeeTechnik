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

        #region Index & Details
        [HttpGet]
        public IActionResult Index(string requestType)
        {
            var requests = _context.ServiceRequests
                .Include(r => r.Machine)
                .ThenInclude(m => m.ObjectEntity)
                .OrderByDescending(r => r.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrEmpty(requestType))
            {
                requests = requests.Where(r => r.RequestType == requestType);
            }

            return View(requests.ToList());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var request = _context.ServiceRequests
                .Include(r => r.Machine)
                .ThenInclude(m => m.ObjectEntity)
                .FirstOrDefault(r => r.Id == id);

            if (request == null) return NotFound();

            return View(request);
        }
        #endregion

        #region Create GET
        [HttpGet]
        public IActionResult CreateMontage()
        {
            return View(new MontageViewModel());
        }

        [HttpGet]
        public IActionResult CreateDemontage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateEmergency()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateMaintenance()
        {
            return View();
        }
        #endregion

        #region Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMontage(MontageViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var obj = _context.Objects.FirstOrDefault(o => o.Name == model.ObjectName)
                ?? new ObjectEntity
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

            if (obj.Id == 0) _context.Objects.Add(obj);
            _context.SaveChanges();

            var machine = _context.Machines.FirstOrDefault(m => m.Model == model.MachineModel && m.ObjectEntityId == obj.Id)
                ?? new Machine
                {
                    Model = model.MachineModel,
                    SerialNumber = model.MachineConnection,
                    ObjectEntityId = obj.Id
                };

            if (machine.Id == 0) _context.Machines.Add(machine);
            _context.SaveChanges();

            var request = new ServiceRequest
            {
                RequestType = "Монтаж",
                MachineId = machine.Id,
                Description = $"Монтаж на машина {machine.Model} на обект {obj.Name}",
                CreatedAt = DateTime.Now,
                Requester = model.Requester
            };

            _context.ServiceRequests.Add(request);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката за монтаж е запазена успешно!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDemontage(string objectName, string requester, string reason)
        {
            if (string.IsNullOrEmpty(objectName) || string.IsNullOrEmpty(requester) || string.IsNullOrEmpty(reason))
            {
                TempData["ErrorMessage"] = "Моля, попълнете всички задължителни полета!";
                return RedirectToAction(nameof(CreateDemontage));
            }

            var obj = _context.Objects.FirstOrDefault(o => o.Name == objectName)
                ?? new ObjectEntity
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

            if (obj.Id == 0) _context.Objects.Add(obj);
            _context.SaveChanges();

            var machine = _context.Machines.FirstOrDefault(m => m.ObjectEntityId == obj.Id)
                ?? new Machine { Model = "Неизвестен модел", ObjectEntityId = obj.Id, SerialNumber = "N/A" };

            if (machine.Id == 0) _context.Machines.Add(machine);
            _context.SaveChanges();

            var request = new ServiceRequest
            {
                RequestType = "Демонтаж",
                MachineId = machine.Id,
                Description = $"Демонтаж на обект {obj.Name}. Причина: {reason}",
                CreatedAt = DateTime.Now,
                Requester = requester
            };

            _context.ServiceRequests.Add(request);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката за демонтаж е запазена успешно!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmergency(string objectName, string requester, string details)
        {
            if (string.IsNullOrEmpty(objectName) || string.IsNullOrEmpty(requester) || string.IsNullOrEmpty(details))
            {
                TempData["ErrorMessage"] = "Моля, попълнете всички задължителни полета!";
                return RedirectToAction(nameof(CreateEmergency));
            }

            var obj = _context.Objects.FirstOrDefault(o => o.Name == objectName)
                ?? new ObjectEntity
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

            if (obj.Id == 0) _context.Objects.Add(obj);
            _context.SaveChanges();

            var machine = _context.Machines.FirstOrDefault(m => m.ObjectEntityId == obj.Id)
                ?? new Machine { Model = "Неизвестен модел", ObjectEntityId = obj.Id, SerialNumber = "N/A" };

            if (machine.Id == 0) _context.Machines.Add(machine);
            _context.SaveChanges();

            var request = new ServiceRequest
            {
                RequestType = "Авария",
                MachineId = machine.Id,
                Description = $"Авария на обект {obj.Name}. Детайли: {details}",
                CreatedAt = DateTime.Now,
                Requester = requester
            };

            _context.ServiceRequests.Add(request);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката за авария е запазена успешно!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMaintenance(string objectName, string requester, string details)
        {
            if (string.IsNullOrEmpty(objectName) || string.IsNullOrEmpty(requester) || string.IsNullOrEmpty(details))
            {
                TempData["ErrorMessage"] = "Моля, попълнете всички задължителни полета!";
                return RedirectToAction(nameof(CreateMaintenance));
            }

            var obj = _context.Objects.FirstOrDefault(o => o.Name == objectName)
                ?? new ObjectEntity
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

            if (obj.Id == 0) _context.Objects.Add(obj);
            _context.SaveChanges();

            var machine = _context.Machines.FirstOrDefault(m => m.ObjectEntityId == obj.Id)
                ?? new Machine { Model = "Неизвестен модел", ObjectEntityId = obj.Id, SerialNumber = "N/A" };

            if (machine.Id == 0) _context.Machines.Add(machine);
            _context.SaveChanges();

            var request = new ServiceRequest
            {
                RequestType = "Профилактика",
                MachineId = machine.Id,
                Description = $"Профилактика на обект {obj.Name}. Детайли: {details}",
                CreatedAt = DateTime.Now,
                Requester = requester
            };

            _context.ServiceRequests.Add(request);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката за профилактика е запазена успешно!";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit & Delete
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var request = _context.ServiceRequests.Find(id);
            if (request == null) return NotFound();

            var model = new EditServiceRequestViewModel
            {
                Id = request.Id,
                Description = request.Description,
                RequestType = request.RequestType,
                Requester = request.Requester
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditServiceRequestViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var request = _context.ServiceRequests.Find(model.Id);
            if (request == null) return NotFound();

            request.Description = model.Description;
            request.RequestType = model.RequestType;
            request.Requester = model.Requester;

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката беше редактирана успешно!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var request = _context.ServiceRequests.Find(id);
            if (request == null)
            {
                TempData["ErrorMessage"] = "Заявката не беше намерена!";
                return RedirectToAction(nameof(Index));
            }

            _context.ServiceRequests.Remove(request);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката беше изтрита успешно!";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}