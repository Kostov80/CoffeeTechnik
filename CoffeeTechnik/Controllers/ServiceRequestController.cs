using Microsoft.AspNetCore.Mvc;

namespace CoffeeTechnik.Controllers
{
    public class ServiceRequestController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        public IActionResult CreateMontage()
        {
            ViewData["RequestType"] = "Монтаж";
           
            return View();
        }

        
        public IActionResult CreateDemontage()
        {
            ViewData["RequestType"] = "Демонтаж";
           
            
            return View();
        }

        
        public IActionResult CreateEmergency()
        {
            ViewData["RequestType"] = "Авария";
            return View();
        }

        
        public IActionResult CreateMaintenance()
        {
            ViewData["RequestType"] = "Профилактика";
           
            return View();
        }

    }
}
