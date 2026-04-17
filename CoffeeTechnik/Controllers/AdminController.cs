using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeTechnik.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                TempData["ErrorMessage"] = "Невалидни данни!";
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Потребителят не е намерен!";
                return RedirectToAction(nameof(Index));
            }
                        
            if (!await _roleManager.RoleExistsAsync(role))
            {
                TempData["ErrorMessage"] = "Ролята не съществува!";
                return RedirectToAction(nameof(Index));
            }
                       
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
            }
                        
            var result = await _userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = "Грешка при задаване на роля!";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Ролята е зададена успешно!";
            return RedirectToAction(nameof(Index));
        }
    }
}