using Microsoft.AspNetCore.Mvc;
using CoffeeTechnik.Attributes;

namespace CoffeeTechnik.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminOnly]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}