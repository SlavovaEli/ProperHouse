using Microsoft.AspNetCore.Mvc;
using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;

namespace ProperHouse.Controllers
{
    public class PropertyController : Controller
    {
        private readonly ICategoryService categoryService;

        private readonly IPropertyService propertyService;
        

        public PropertyController(IPropertyService _propertyService, ICategoryService _categoryService)
        {
            categoryService = _categoryService;
            propertyService = _propertyService;
        }

        //public IActionResult Add() => View();

        public IActionResult Add() => View(new PropertyAddViewModel
        {
            Categories = categoryService.GetPropertyCategories()
        });

        [HttpPost]
        public IActionResult Add(PropertyAddViewModel property)
        {
            if (categoryService.CategoryExists(property.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(property.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {
                property.Categories = categoryService.GetPropertyCategories();

                return View(property);
            }

            var newProperty = new Property
            {
                CategoryId = property.CategoryId,
                Town = property.Town,
                Quarter = property.Quarter,
                Capacity = property.Capacity,
                Area = property.Area,
                Floor = property.Floor,
                Price = property.Price,
                Description = property.Description,
                ImageUrl = property.ImageUrl,
            };

            propertyService.AddProperty(newProperty);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            var propertiesToList = propertyService.GetAllProperties();                

            return View(propertiesToList);
        }

        public IActionResult Find() => View(new PropertySearchViewModel
        {
            Categories = categoryService.GetPropertyCategories(),
            Towns = propertyService.FindAllTowns()
        });

        public IActionResult Found([FromQuery]PropertySearchViewModel search)
        {
            var foundProperties = propertyService.FindProperties(search);                

            return View(foundProperties);
        }
    }
}
