using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models;
using ProperHouse.Core.Models.Favorite;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;
using ProperHouse.Infrastructure.Extensions;
using ProperHouse.Core.Constants;

namespace ProperHouse.Controllers
{
    public class PropertyController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IPropertyService propertyService;
        private readonly IOwnerService ownerService;
        private readonly IUserService userService;

        public PropertyController(IPropertyService _propertyService,
            ICategoryService _categoryService,
            IOwnerService _ownerService,
            IUserService _userService)
        {
            categoryService = _categoryService;
            propertyService = _propertyService;
            ownerService = _ownerService;
            userService = _userService;
        }

        [Authorize]
        public IActionResult Add()
        {
            string userId = this.User.GetId();

            if (!ownerService.IsUserOwner(userId))
            {
                return RedirectToAction("Create", "Owner");
            }

            return View(new PropertyViewModel
            {
                Categories = categoryService.GetPropertyCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(PropertyViewModel property)
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

            var propertyId = propertyService.AddProperty(userId, property);

            TempData[WebConstants.MessageKey] = "Your property is waiting for approval!";

            return RedirectToAction(nameof(Details), new { id = propertyId});
        }

        public IActionResult All()
        {
            var propertiesToList = propertyService.GetPublicProperties();

            return View(propertiesToList);
        }

        public IActionResult Find() => View(new PropertySearchViewModel
        {
            Categories = categoryService.GetPropertyCategories(),
            Towns = propertyService.FindAllTowns()
        });

        public IActionResult Found(PropertySearchViewModel search)
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
        
        [Authorize]
        public IActionResult Details(int id)
        {
            var propertyModel= propertyService.Details(id);

            return View(propertyModel);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = User.GetId();

            if (!ownerService.IsUserOwner(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(OwnerController.Create), "Owner");
            }

            var property = propertyService.GetProperty(id);

            var owner = ownerService.GetPropertyOwner(property);

            if(owner.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var propertyEditForm = new PropertyViewModel()
            {
                Id = id,
                CategoryId = property.CategoryId,
                ImageUrl = property.ImageUrl,
                Price = property.Price,
                Area = property.Area,
                Capacity = property.Capacity,
                Description = property.Description,
                Floor = property.Floor,
                Quarter = property.Quarter,
                Town = property.Town
            };

            propertyEditForm.Categories = categoryService.GetPropertyCategories();

            return View(propertyEditForm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, [FromForm] PropertyViewModel property)
        {
            var ownerId = ownerService.GetOwnerId(this.User.GetId());

            if(ownerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(OwnerController.Create), "Owner");
            }

            if (!categoryService.CategoryExists(property.CategoryId))
            {
                ModelState.AddModelError(nameof(property.CategoryId), "Category does not exist");
            }

            if (!ModelState.IsValid)
            {
                property.Categories = categoryService.GetPropertyCategories();

                return View(property);
            }

            if(!propertyService.PropertyIsOwners(id, ownerId) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            bool isAdmin = User.IsAdmin();

            var edited = propertyService.Edit(id, isAdmin, property);

            if(!edited)
            {
                return BadRequest();
            }

            if(!isAdmin)
            {
                TempData[WebConstants.MessageKey] = "Your property was editted and is waiting for approval!";
            }
            

            return RedirectToAction(nameof(PropertyController.Details), new {id = id});
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = User.GetId();  

            propertyService.Delete(userId, id);

            if(User.IsAdmin())
            {
                return RedirectToAction("All", "Property", new { area = "Admin" });
            }

            return RedirectToAction(nameof(All));
        }
    }
}
