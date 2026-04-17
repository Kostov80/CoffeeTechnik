using CoffeeTechnik.Services.Interfaces;
using CoffeeTechnik.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeTechnik.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly IServiceRequestService _service;

        public ServiceRequestController(IServiceRequestService service)
        {
            _service = service;
        }
                
        [HttpGet]
        public IActionResult Index(string requestType, string searchString, int page = 1)
        {
            var result = _service.GetAll(requestType, searchString, page);

            ViewBag.CurrentPage = result.CurrentPage;
            ViewBag.TotalPages = result.TotalPages;
            ViewBag.CurrentFilter = requestType;
            ViewBag.SearchString = searchString;

            return View(result.Items);
        }

        
        [HttpGet]
        public IActionResult Details(int id)
        {
            var request = _service.GetById(id);

            if (request == null)
                return NotFound();

            return View(request);
        }

                
        [HttpGet]
        public IActionResult CreateMontage() => View(new MontageViewModel());

        [HttpGet]
        public IActionResult CreateDemontage() => View();

        [HttpGet]
        public IActionResult CreateEmergency() => View();

        [HttpGet]
        public IActionResult CreateMaintenance() => View();

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMontage(MontageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _service.CreateMontage(model);

            TempData["SuccessMessage"] = "Заявката за монтаж е създадена!";
            return RedirectToAction(nameof(Index));
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDemontage(string objectName, string requester, string reason)
        {
            if (string.IsNullOrWhiteSpace(objectName) ||
                string.IsNullOrWhiteSpace(requester) ||
                string.IsNullOrWhiteSpace(reason))
            {
                TempData["ErrorMessage"] = "Попълни всички полета!";
                return RedirectToAction(nameof(CreateDemontage));
            }

            _service.CreateDemontage(objectName, requester, reason);

            TempData["SuccessMessage"] = "Демонтаж заявка създадена!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmergency(string objectName, string requester, string details)
        {
            if (string.IsNullOrWhiteSpace(objectName) ||
                string.IsNullOrWhiteSpace(requester) ||
                string.IsNullOrWhiteSpace(details))
            {
                TempData["ErrorMessage"] = "Попълни всички полета!";
                return RedirectToAction(nameof(CreateEmergency));
            }

            _service.CreateEmergency(objectName, requester, details);

            TempData["SuccessMessage"] = "Авария заявка създадена!";
            return RedirectToAction(nameof(Index));
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMaintenance(string objectName, string requester, string details)
        {
            if (string.IsNullOrWhiteSpace(objectName) ||
                string.IsNullOrWhiteSpace(requester) ||
                string.IsNullOrWhiteSpace(details))
            {
                TempData["ErrorMessage"] = "Попълни всички полета!";
                return RedirectToAction(nameof(CreateMaintenance));
            }

            _service.CreateMaintenance(objectName, requester, details);

            TempData["SuccessMessage"] = "Профилактика заявка създадена!";
            return RedirectToAction(nameof(Index));
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var success = _service.Delete(id);

            if (!success)
            {
                TempData["ErrorMessage"] = "Заявката не е намерена!";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Изтрита успешно!";
            return RedirectToAction(nameof(Index));
        }
    }
}