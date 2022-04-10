using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;
using ProperHouse.Infrastructure.Extensions;

namespace ProperHouse.Controllers
{
    public class PropertyController : Controller
    {
        private readonly ICategoryService categoryService;

        private readonly IPropertyService propertyService;

        private readonly IOwnerService ownerService;
        

        public PropertyController(IPropertyService _propertyService, 
            ICategoryService _categoryService,
            IOwnerService _ownerService)
        {
            categoryService = _categoryService;
            propertyService = _propertyService;
            ownerService = _ownerService;
        }

        [Authorize]
        public IActionResult Add() 
        {
            string userId = this.User.GetId();

            if(!ownerService.IsUserOwner(userId))
            {
                return RedirectToAction("Create", "Owner");
            }

            return View(new PropertyAddViewModel
            {
                Categories = categoryService.GetPropertyCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(PropertyAddViewModel property)
        {
            string userId = this.User.GetId();            

            if (!ownerService.IsUserOwner(userId))
            {
                return RedirectToAction("Create", "Owner");
            }            

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
                OwnerId = ownerService.GetOwnerId(userId)
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

        [Authorize]
        public IActionResult Mine()
        {
            var myProperties = propertyService.MyProperties(this.User.GetId());

            return View(myProperties);
        }

        public IActionResult Details(int id)
        {
            var property = propertyService.Details(id);

            return View(property);
        }
    }
}
