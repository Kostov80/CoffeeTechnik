using Microsoft.AspNetCore.Mvc;
using CoffeeTechnik.Models;

namespace CoffeeTechnik.Controllers
{
    public class ServiceRequestController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();//начална страница 
        }

        
        [HttpGet]
        public IActionResult CreateMontage()// станица за монтаж
        {
            ViewData["PageTitle"] = "Страница за Монтаж";

            return View(new MontageViewModel()); 
        }

        
        [HttpPost]
        public IActionResult CreateMontage(MontageViewModel model) //обработка на данните 
        {
            ViewData["PageTitle"] = "Страница за Монтаж";

            if (!ModelState.IsValid)
            {
                
                return View(model);//грешка при празно поле
            }

            
            TempData["SuccessMessage"] = "Заявката е запаметена успешно!";

            
            return RedirectToAction("Create");// след запазване начална страница
        }

        
        [HttpGet]
        public IActionResult CreateDemontage()
        {
            ViewData["RequestType"] = "Демонтаж";

            return View();
        }

        [HttpGet]
        public IActionResult CreateEmergency()
        {
            ViewData["RequestType"] = "Авария";

            return View();
        }

        [HttpGet]
        public IActionResult CreateMaintenance()
        {
            ViewData["RequestType"] = "Профилактика";

            return View();
        }
    }
}
