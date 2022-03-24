using Microsoft.AspNetCore.Mvc;
using ProperHouse.Core.Models;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;

namespace ProperHouse.Controllers
{
    public class PropertyController : Controller
    {
        private readonly ProperHouseDbContext dbContext;

        public PropertyController(ProperHouseDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public IActionResult Add() => View();

        /*public IActionResult Add() => View(new PropertyAddViewModel
        {
            Categories = GetPropertyCategories()
        });*/

        [HttpPost]
        public IActionResult Add(PropertyAddViewModel property)
        {
            if(!dbContext.Categories.Any(c => c.Id == property.CategoryId))
            {
                ModelState.AddModelError(nameof(property.CategoryId), "Category does not exist");
            }

            if(!ModelState.IsValid)
            {
                property.Categories = GetPropertyCategories();

                return View(property);
            }

            var newProperty = new Property
            {
                CategoryId = property.CategoryId,
                Town = property.Town,
                Quarter = property.Quarter,
                Area = property.Area,
                Floor = property.Floor,
                Price = property.Price,
                Description = property.Description,
                ImageUrl = property.ImageUrl,
            };

            dbContext.Add(newProperty);
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private List<PropertyCategoryViewModel> GetPropertyCategories()
        {
            return dbContext.Categories
                .Select(c => new PropertyCategoryViewModel
                {
                    Name = c.Name,
                    Id = c.Id,
                })
                .ToList();
        }
    }
}
