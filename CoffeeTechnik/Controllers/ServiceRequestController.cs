using Microsoft.AspNetCore.Mvc;
using CoffeeTechnik.Data;
using CoffeeTechnik.Models;
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


        #region Index

        [HttpGet]
        public IActionResult Index(string requestType)//  всички заявки
        {
            var requests = _context.ServiceRequests.Include(r => r.Machine)
                                   .ThenInclude(m => m.ObjectEntity).OrderByDescending(r => r.CreatedAt)
                                   .AsQueryable();

            if (!string.IsNullOrEmpty(requestType))
            {

                requests = requests.Where(r => r.RequestType == requestType);
            }


            return View(requests.ToList());
        }

        #endregion

        #region Create Views

        [HttpGet]
        public IActionResult Create()// избор на тип заявка
        {
            
            ViewBag.Objects = _context.Objects.ToList();

            ViewBag.Machines = _context.Machines.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult CreateMontage()//  монтаж
        {
            ViewData["PageTitle"] = "Заявка за Монтаж";

           
            if (TempData["SuccessMessage"] != null)
            {

                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }

            return View(new MontageViewModel()); 
        }

        [HttpGet]
        public IActionResult CreateDemontage()//  демонтаж
        {
            ViewData["PageTitle"] = "Заявка за Демонтаж";

            ViewBag.Objects = _context.Objects.ToList();

            ViewBag.Machines = _context.Machines.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult CreateEmergency()//  авария
        {
            ViewData["PageTitle"] = "Заявка за Авария";

            ViewBag.Objects = _context.Objects.ToList();

            ViewBag.Machines = _context.Machines.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult CreateMaintenance()//  профилактика
        {
            ViewData["PageTitle"] = "Заявка за Профилактика";

            ViewBag.Objects = _context.Objects.ToList();

            ViewBag.Machines = _context.Machines.ToList();

            return View();
        }

        #endregion

        #region Create POST Actions





       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMontage(MontageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }

            // 1. Взимаме или създаваме обект
            var obj = _context.Objects.FirstOrDefault(o => o.Name == model.ObjectName);// обект с това име вече съществува ли
           
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

            
           
            var machine = _context.Machines.FirstOrDefault(m => m.Model == model.MachineModel && m.ObjectEntityId == obj.Id);// машина с този модел вече е монтирана на този обект ли
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

            
            var request = new ServiceRequest// създаваме заявка за монтаж
            {
                RequestType = "Монтаж",

                MachineId = machine.Id,

                Description = $"Монтаж на машина {machine.Model} на обект {obj.Name}",

                CreatedAt = DateTime.Now
            };

            _context.ServiceRequests.Add(request);

            _context.SaveChanges();

            
            TempData["SuccessMessage"] = "Заявката за монтаж е запаметена успешно!";

            
            return RedirectToAction(nameof(CreateMontage));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDemontage(string objectName, string requester, string reason, string technician, string note)
        {
            if (string.IsNullOrEmpty(objectName) || string.IsNullOrEmpty(requester))
            {
                TempData["ErrorMessage"] = "Моля, попълнете задължителните полета!";

                return RedirectToAction("CreateDemontage");
            }

           
            var obj = _context.Objects.FirstOrDefault(o => o.Name == objectName);
            
            if (obj == null)
            {
                obj = new ObjectEntity { Name = objectName, Type = "Демонтаж" };

                _context.Objects.Add(obj);

                _context.SaveChanges();
            }

            var request = new ServiceRequest
            {
                RequestType = "Демонтаж",

                Description = $"Демонтаж на обект {obj.Name}. Причина: {reason}",

                CreatedAt = DateTime.Now
            };

            _context.ServiceRequests.Add(request);

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката за демонтаж е запаметена успешно!";
            return RedirectToAction("CreateDemontage"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmergency(string objectName, string emergencyDetails)
        {
            if (string.IsNullOrEmpty(objectName) || string.IsNullOrEmpty(emergencyDetails))
            {
                TempData["ErrorMessage"] = "Моля, попълнете задължителните полета!";

                return RedirectToAction("CreateEmergency");
            }

           
            var obj = _context.Objects.FirstOrDefault(o => o.Name == objectName);
            
            if (obj == null)
            {
                obj = new ObjectEntity { Name = objectName, Type = "Авария" };

                _context.Objects.Add(obj);

                _context.SaveChanges();
            }

            var request = new ServiceRequest
            {
                RequestType = "Авария",

                Description = $"Авария на обект {obj.Name}. Детайли: {emergencyDetails}",

                CreatedAt = DateTime.Now
            };

            _context.ServiceRequests.Add(request);

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката за авария е запаметена успешно!";

            return RedirectToAction("CreateEmergency"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMaintenance(string objectName, string requestFrom)
        {
            if (string.IsNullOrEmpty(objectName) || string.IsNullOrEmpty(requestFrom))
            {
                TempData["ErrorMessage"] = "Моля, попълнете задължителните полета!";

                return RedirectToAction("CreateMaintenance");
            }

          
            var obj = _context.Objects.FirstOrDefault(o => o.Name == objectName);
           
            if (obj == null)
            {
                obj = new ObjectEntity { Name = objectName, Type = "Профилактика" };

                _context.Objects.Add(obj);

                _context.SaveChanges();
            }

            var request = new ServiceRequest
            {
                RequestType = "Профилактика",

                Description = $"Профилактика на обект {obj.Name}. От: {requestFrom}",

                CreatedAt = DateTime.Now
            };

            _context.ServiceRequests.Add(request);

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката за профилактика е запаметена успешно!";

            return RedirectToAction("CreateMaintenance"); 
        }

        #endregion

        #region Edit & Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
           
            var request = _context.ServiceRequests.Find(id);
            if (request == null)
            {
                TempData["ErrorMessage"] = "Заявката не беше намерена!";

                return RedirectToAction("Index");
            }

            _context.ServiceRequests.Remove(request);

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката беше изтрита успешно!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteObject([FromBody] DeleteObjectViewModel model)
        {

            if (string.IsNullOrEmpty(model.Name))
            {
                return Json(new { success = false, message = "Моля, въведете име на обекта!" });
            }

            
            var obj = _context.Objects.FirstOrDefault(o => o.Name == model.Name);// намери обекта по име
           
            if (obj == null)
            {
                return Json(new { success = false, message = "Обектът не беше намерен!" });
            }

            
            var machines = _context.Machines.Where(m => m.ObjectEntityId == obj.Id).ToList();
           
            foreach (var machine in machines)
            {
                var requests = _context.ServiceRequests.Where(r => r.MachineId == machine.Id).ToList();

                _context.ServiceRequests.RemoveRange(requests);
            }
            _context.Machines.RemoveRange(machines);

            _context.Objects.Remove(obj);

            _context.SaveChanges();

            return Json(new { success = true, message = $"Обектът '{model.Name}' беше изтрит успешно!" });
        }


        [HttpGet]
        public IActionResult Edit(int id)// страница за редактиране на заявка
        {
            var request = _context.ServiceRequests

                .Include(r => r.Machine)

                .ThenInclude(m => m.ObjectEntity)

                .FirstOrDefault(r => r.Id == id);

            if (request == null)
                return NotFound();

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ServiceRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            
            var request = _context.ServiceRequests.Find(model.Id);
           
            if (request == null)
                return NotFound();

            request.Description = model.Description;
            request.RequestType = model.RequestType;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion
    }
}
