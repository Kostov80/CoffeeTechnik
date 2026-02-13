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

        public IActionResult Index()//  всички заявки
        {
            var requests = _context.ServiceRequests
                .OrderByDescending(r => r.CreatedAt)
                .ToList();


            return View(requests);
        }


        public IActionResult Details(int id)// страница с детайли за заявка
        {
            var request = _context.ServiceRequests
                .FirstOrDefault(r => r.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
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
        public IActionResult CreateMontage(MontageViewModel model)//   заявка за монтаж
        {
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }

            
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

                CreatedAt = DateTime.Now,

                Requester = model.Requester
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
            
            if (string.IsNullOrEmpty(objectName) || string.IsNullOrEmpty(requester))//задължителните полета
            {
                TempData["ErrorMessage"] = "Моля, попълнете задължителните полета!";

                return RedirectToAction("CreateDemontage");
            }

            
           
            var obj = _context.Objects.FirstOrDefault(o => o.Name == objectName);// Вземаме или създаваме обекта
            
            if (obj == null)
            {
                obj = new ObjectEntity
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
            }

            
            var machine = _context.Machines.FirstOrDefault(m => m.ObjectEntityId == obj.Id);
            
            if (machine == null)
            {
                machine = new Machine
                {
                    Model = "Неизвестен модел",      
                   
                    ObjectEntityId = obj.Id,
                   
                    SerialNumber = note ?? "N/A"
                };

                _context.Machines.Add(machine);

                _context.SaveChanges(); 
            }

            
            var request = new ServiceRequest// заявката за демонтаж
            {
                RequestType = "Демонтаж",

                MachineId = machine.Id,   
                
                Description = $"Демонтаж на обект {obj.Name}. Причина: {reason}",

                CreatedAt = DateTime.Now,

                Requester = requester                  
            };

            _context.ServiceRequests.Add(request);

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката за демонтаж е запаметена успешно!";

            return RedirectToAction("CreateDemontage");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmergency(string objectName, string emergencyDetails)// заявка за авария
        {
            
            if (string.IsNullOrEmpty(objectName) || string.IsNullOrEmpty(emergencyDetails))// задължителните полета
            {
                TempData["ErrorMessage"] = "Моля, попълнете задължителните полета!";

                return RedirectToAction("CreateEmergency");
            }

            
            var obj = _context.Objects.FirstOrDefault(o => o.Name == objectName);// Вземаме или създаваме обекта
           
            if (obj == null)
            {
                obj = new ObjectEntity
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
            }

            
            var machine = _context.Machines.FirstOrDefault(m => m.ObjectEntityId == obj.Id);
           
            if (machine == null)
            {
                machine = new Machine
                {
                    Model = "Неизвестен модел", 

                    ObjectEntityId = obj.Id,

                    SerialNumber = "N/A"
                };

                _context.Machines.Add(machine);

                _context.SaveChanges(); 
            }

            
            var request = new ServiceRequest// заявката за авария
            {
                RequestType = "Авария",

                MachineId = machine.Id,  
                
                Description = $"Авария на обект {obj.Name}. Детайли: {emergencyDetails}",

                CreatedAt = DateTime.Now,

                Requester = "N/A"                     
            };

            _context.ServiceRequests.Add(request);

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Заявката за авария е запаметена успешно!";

            return RedirectToAction("CreateEmergency");
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMaintenance(string objectName, string requestFrom)// заявка за профилактика
        {
            
            if (string.IsNullOrEmpty(objectName) || string.IsNullOrEmpty(requestFrom))
            {
                TempData["ErrorMessage"] = "Моля, попълнете задължителните полета!";

                return RedirectToAction("CreateMaintenance");
            }

            
            var obj = _context.Objects.FirstOrDefault(o => o.Name == objectName);
           
            if (obj == null)
            {
                obj = new ObjectEntity
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
            }

            
            var machine = _context.Machines.FirstOrDefault(m => m.ObjectEntityId == obj.Id);
           
            if (machine == null)
            {
                machine = new Machine
                {
                    Model = "Неизвестен модел", 

                    ObjectEntityId = obj.Id,

                    SerialNumber = "N/A"
                };

                _context.Machines.Add(machine);

                _context.SaveChanges(); 
            }

            
            var request = new ServiceRequest// заявката за профилактика
            {
                RequestType = "Профилактика",

                MachineId = machine.Id,        
                
                Description = $"Профилактика на обект {obj.Name}. От: {requestFrom}",

                CreatedAt = DateTime.Now,

                Requester = requestFrom                    
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
        public IActionResult Delete(int id)// изтриване на заявка
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

        [HttpGet]
        public IActionResult DeleteObject()// страница за изтриване на обект
        {
            return View(new DeleteObjectViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteObject(DeleteObjectViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                TempData["ErrorMessage"] = "Моля, въведете име на обекта!";

                return View(model);
            }

            var obj = _context.Objects
                .FirstOrDefault(o => o.Name.ToLower() == model.Name.ToLower());

            if (obj == null)
            {
                TempData["ErrorMessage"] = "Обектът не беше намерен!";

                return View(model);
            }

            
            var machines = _context.Machines
                .Where(m => m.ObjectEntityId == obj.Id)
                .ToList();

            foreach (var machine in machines)
            {
                var requests = _context.ServiceRequests
                    .Where(r => r.MachineId == machine.Id)
                    .ToList();


                _context.ServiceRequests.RemoveRange(requests);
            }

            _context.Machines.RemoveRange(machines);

            _context.Objects.Remove(obj);

            _context.SaveChanges();

            TempData["SuccessMessage"] = $"Обектът '{model.Name}' беше изтрит успешно!";

            return RedirectToAction("DeleteObject");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var request = _context.ServiceRequests.FirstOrDefault(r => r.Id == id);
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
        public IActionResult Edit(EditServiceRequestViewModel model)// редактиране на заявка
        {
            if (!ModelState.IsValid)
                return View(model);

            var request = _context.ServiceRequests.Find(model.Id);
           
            if (request == null) return NotFound();

            request.Description = model.Description;

            request.RequestType = model.RequestType;

            request.Requester = model.Requester;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        #endregion
    }
}
