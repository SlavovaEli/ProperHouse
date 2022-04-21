using Microsoft.AspNetCore.Mvc;
using ProperHouse.Core.Contracts;

namespace ProperHouse.Areas.Admin.Controllers
{
    public class PropertyController : AdminController
    {
        private readonly IPropertyService propertyService;

        public PropertyController(IPropertyService _propertyService)
        {
            propertyService = _propertyService;
        }
        public IActionResult All()
        {
            var properties = propertyService.GetAllProperties();

            return View(properties);
        }
    }
}
