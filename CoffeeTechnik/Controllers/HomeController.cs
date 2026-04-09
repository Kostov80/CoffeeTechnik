using CoffeeTechnik.Models;
using CoffeeTechnik.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoffeeTechnik.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
                
        public IActionResult Index()
        {
            //throw new Exception("Test error");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
             
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
               
        public IActionResult Error500()
        {
            return View("~/Views/Shared/Error500.cshtml");
        }
               
        public IActionResult StatusCode(int code)
        {
            if (code == 404)
            {
                return View("~/Views/Shared/NotFound.cshtml");
            }

            return RedirectToAction("Error500");
        }
                
        public string GetUserRole()
        {
            return TempData["UserRole"] as string ?? "Guest";
        }

        public IActionResult CheckAccess(string actionName)
        {
            string role = GetUserRole();
            TempData.Keep("UserRole");

            if (role == "Technician") return null;

            if (role == "Sales")
            {
                if (actionName == "Index" || actionName == "Create")
                    return null;
            }

            return RedirectToAction("AccessDenied");
        }
    }
}