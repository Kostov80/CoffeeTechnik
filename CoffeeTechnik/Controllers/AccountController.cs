using Microsoft.AspNetCore.Mvc;
using CoffeeTechnik.Models;

namespace CoffeeTechnik.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login() // Показване на входа
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model) // Обработване на входа
        {
            if (!ModelState.IsValid)
            {
               
                return View(model);
            }

            
            if (model.Username == "tech" && model.Password == "1234")// Проверка на  име и парола
            {
                TempData["UserRole"] = "Technician";
                TempData.Keep("UserRole");

                return RedirectToAction("Index", "Home");
            }

            else if (model.Username == "sales" && model.Password == "1234")
            {

                TempData["UserRole"] = "Sales";
                TempData.Keep("UserRole");

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Грешно потребителско име или парола");
            return View(model);
        }

        [HttpGet]
        public IActionResult GuestLogin()// Влизане като гост
        {
            
            TempData["UserRole"] = "Guest";
            TempData.Keep("UserRole");

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            
            TempData["UserRole"] = "Guest";
            TempData.Keep("UserRole");

            return RedirectToAction("Login", "Account");
        }
    }
}
