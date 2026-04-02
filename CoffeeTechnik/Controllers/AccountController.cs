using Microsoft.AspNetCore.Mvc;
using CoffeeTechnik.Models;
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

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var username = model.Username.Trim();

            var user = _users.FirstOrDefault(u =>
                u.Username.ToLower() == username.ToLower() &&
                u.Password == model.Password &&
                u.Role == model.Role);

            if (user == null)
            {
                ModelState.AddModelError("", "Невалидни данни за вход");
                return View(model);
            }

            TempData["UserRole"] = user.Role;
            TempData.Keep("UserRole");

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

            var username = model.Username.Trim();

            
            if (_users.Any(u => u.Username.ToLower() == username.ToLower()))
            {
                ModelState.AddModelError("Username", "Това потребителско име вече съществува");
                return View(model);
            }

            _users.Add(new RegisterViewModel
            {
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim(),
                PhoneNumber = model.PhoneNumber.Trim(),
                Username = username,
                Password = model.Password,
                Role = model.Role
            });

            
            TempData["SuccessMessage"] = "Регистрацията е успешна!";

            
            TempData["UserRole"] = model.Role;
            TempData.Keep("UserRole");

            return RedirectToAction("Index", "Home");
        }

        
        [HttpGet]
        public IActionResult GuestLogin()
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