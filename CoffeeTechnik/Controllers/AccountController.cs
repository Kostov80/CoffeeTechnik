using Microsoft.AspNetCore.Mvc;
using CoffeeTechnik.Models;

namespace CoffeeTechnik.Controllers
{
    public class AccountController : Controller
    {
        

        [HttpGet]
        public IActionResult Login()   // GET заявка за показване на формата за вход
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Username == "tech" && model.Password == "1234")
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
        public IActionResult GuestLogin()// GET заявка за вход като гост
        {
            TempData["UserRole"] = "Guest";

            TempData.Keep("UserRole");

            return RedirectToAction("Index", "Home");
        }

        

        [HttpGet] 
        public IActionResult Logout()
        {
            TempData.Clear(); 

            return RedirectToAction("Login", "Account");
        }
    }
}
