using System.Diagnostics;
using CoffeeTechnik.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeTechnik.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index()// Начална страница
        {
            return View();
        }

        
        public IActionResult Privacy()// Privacy
        {
            
            return View();
        }

        
        public IActionResult AccessDenied()// Страница при достъп отказан
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        public string GetUserRole() // Пример как да вземеш ролята от TempData/Session
        {
            
            return TempData["UserRole"] as string ?? "Guest";
        }

        
        public IActionResult CheckAccess(string actionName)// Проверка за достъп
        {
            
            string role = GetUserRole();
            TempData.Keep("UserRole"); // за следващи заявки

            
            
            if (role == "Technician") return null; // Техник има достъп навсякъде
            
            if (role == "Sales")
            {
                
                if (actionName == "Index" || actionName == "Create") // Търговец може само Index и Create
                    return null;
            }
            
           
            
            return RedirectToAction("AccessDenied"); 
        }
    }
}
