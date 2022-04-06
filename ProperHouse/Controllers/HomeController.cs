using Microsoft.AspNetCore.Mvc;
using ProperHouse.Core.Contracts;
using ProperHouse.Models;
using System.Diagnostics;

namespace ProperHouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropertyService propertyService;

        public HomeController(IPropertyService _propertyService)
        {
            propertyService = _propertyService;
        }

        public IActionResult Index()
        {
            var latestProperties = propertyService.GetAllProperties()
                .OrderByDescending(p => p.Id)
                .Take(3)
                .ToList();

            return View(latestProperties);
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}