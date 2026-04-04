using CoffeeTechnik.Models;
using CoffeeTechnik.ViewModels; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeTechnik.Controllers
{
    public class AccountController : Controller
    {
        private static List<RegisterViewModel> _users = new List<RegisterViewModel>
        {
            new RegisterViewModel
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                PhoneNumber = "0888888888",
                Username = "tech",
                Password = "1234",
                Role = "Technician"
            },
            new RegisterViewModel
            {
                FirstName = "Petar",
                LastName = "Petrov",
                PhoneNumber = "0877777777",
                Username = "sales",
                Password = "1234",
                Role = "Sales"
            }
        };

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _users.FirstOrDefault(u =>
                u.Username == model.Username &&
                u.Password == model.Password &&
                u.Role == model.Role);

            if (user == null)
            {
                ModelState.AddModelError("", "Грешно потребителско име, парола или роля");
                return View(model);
            }

            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Това потребителско име вече съществува");
                return View(model);
            }

            _users.Add(model);
            TempData["SuccessMessage"] = "Регистрацията е успешна!";
            HttpContext.Session.SetString("UserRole", model.Role);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GuestLogin()
        {
            HttpContext.Session.SetString("UserRole", "Guest");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}