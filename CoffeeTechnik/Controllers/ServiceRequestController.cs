using Microsoft.AspNetCore.Mvc;
using CoffeeTechnik.Models;

namespace CoffeeTechnik.Controllers
{
    public class ServiceRequestController : Controller
    {
        
        [HttpGet]
        public IActionResult Create() // страница за избор на заявка
        {
            ViewData["PageTitle"] = "Нова заявка";

            return View();
        }

        
        [HttpGet]
        public IActionResult CreateMontage()// монтаж
        {
            ViewData["PageTitle"] = "Заявка за Монтаж";

            return View(new MontageViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMontage(MontageViewModel model)
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
        public IActionResult CreateDemontage()// демонтаж
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
        public IActionResult CreateEmergency()// авария
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
        public IActionResult CreateMaintenance(string objectName, string requestFrom)// профилактика
        {
            if (string.IsNullOrEmpty(objectName) || string.IsNullOrEmpty(requestFrom))
            {
                TempData["ErrorMessage"] = "Моля, попълнете задължителните полета!";

                return RedirectToAction("CreateMaintenance");
            }

            TempData["SuccessMessage"] = "Заявката за профилактика е запаметена успешно!";

            return RedirectToAction("Create");
        }
    }
}
