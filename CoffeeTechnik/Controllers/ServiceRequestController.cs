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

        
        [HttpGet]
        public IActionResult Create()//нова заявка
        {
            ViewData["PageTitle"] = "Нова заявка";

            return View();
        }

        
        [HttpGet]
        public IActionResult Index(string requestType)//списък със заявки
        {
            var requests = _context.ServiceRequests.Include(r => r.Machine)
                           .OrderByDescending(r => r.Id).AsQueryable();

           
            
            if (!string.IsNullOrEmpty(requestType))
            {

                requests = requests.Where(r => r.RequestType == requestType);
            }

            return View(requests.ToList());
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)//изтриване на заявка
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

        #region Заявки по тип

        [HttpGet]
        public IActionResult CreateMontage()//монтаж
        {
            ViewData["PageTitle"] = "Заявка за Монтаж";

            return View(new MontageViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMontage(MontageViewModel model)//монтаж
        {
            ViewData["PageTitle"] = "Заявка за Монтаж";

            if (!ModelState.IsValid)
            {

                return View(model);
            }

            
            TempData["SuccessMessage"] = "Заявката за монтаж е запаметена успешно!";

            return RedirectToAction("Create");
        }

        [HttpGet]
        public IActionResult CreateDemontage()
        {
            ViewData["PageTitle"] = "Заявка за Демонтаж";

            return View();
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

            TempData["SuccessMessage"] = "Заявката за демонтаж е запаметена успешно!";

            return RedirectToAction("Create");
        }

        [HttpGet]
        public IActionResult CreateEmergency()
        {
            ViewData["PageTitle"] = "Заявка за Авария";

            return View();
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

            TempData["SuccessMessage"] = "Заявката за авария е запаметена успешно!";

            return RedirectToAction("Create");
        }

        [HttpGet]
        public IActionResult CreateMaintenance()
        {
            ViewData["PageTitle"] = "Заявка за Профилактика";

            return View();
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

            TempData["SuccessMessage"] = "Заявката за профилактика е запаметена успешно!";

            return RedirectToAction("Create");
        }

        #endregion
    }
}
