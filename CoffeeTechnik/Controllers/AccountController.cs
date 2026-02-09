using Microsoft.AspNetCore.Mvc;
using CoffeeTechnik.Models;

namespace CoffeeTechnik.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()// Показване на  входа
        {
         
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)//обработване на входа
        {
            if (!ModelState.IsValid)
            {
                return  View(model);
            }

            
            if (model.Username == "tech" && model.Password == "1234")// Проверка на потребителското име и паролата 
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Грешно потребителско име или парола");// Грешно потребителско име или парола
            return View(model);
        }

        [HttpGet]
        public IActionResult GuestLogin()
        {
            
             return RedirectToAction("Index", "Home");// Влизане като гост 
        }
    }
}
