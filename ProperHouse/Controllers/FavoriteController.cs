using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models;
using ProperHouse.Core.Models.Favorite;
using ProperHouse.Infrastructure.Data.Models;
using ProperHouse.Infrastructure.Extensions;

namespace ProperHouse.Controllers
{
    public class FavoriteController : BaseController
    {
        private readonly IFavoriteService favoriteService;

        private readonly IPropertyService propertyService;

        private readonly ICategoryService categoryService;

        public FavoriteController(IPropertyService _propertyService,
            IFavoriteService _favoriteService,
            ICategoryService _categoryService)
        {
            propertyService = _propertyService;
            favoriteService = _favoriteService;
            categoryService = _categoryService;
        }
        
        public IActionResult AddFavorite(int id)
        {
            var userId = this.User.GetId();
            var property = propertyService.GetProperty(id);

            var favoriteModel = new FavoriteViewModel
            {
                Id = id,
                UserId = userId,
                Category = categoryService.GetCategoryName(property.CategoryId),
                Town = property.Town,
                Quarter = property.Quarter,
                Capacity = property.Capacity,
                Price = property.Price
            };

            return View(favoriteModel);

        }

        [HttpPost]        
        public IActionResult AddFavorite(int id, [FromForm] FavoriteViewModel form)
        {
            var userId = User.GetId();

            var favorite = new Favorite
            {
                UserId = userId,
                PropertyId = id
            };

            favoriteService.AddToFavorites(favorite);

            return RedirectToAction("MyFavorites");
        }

        public IActionResult MyFavorites()
        {
            var userId = User.GetId();

            var favoriteModel = favoriteService.GetFavorites(userId)
                .Select(p => new PropertyListingViewModel
                {
                    Id = p.Id,
                    Category = categoryService.GetCategoryName(p.CategoryId),
                    Town= p.Town,
                    Capacity = p.Capacity,
                    ImageUrl = p.ImageUrl
                })
                .ToList();

            return View(favoriteModel);
        }


        public IActionResult RemoveFavorite(int id)
        {
            var userId = User.GetId();
            favoriteService.Remove(userId, id);

            return RedirectToAction(nameof(MyFavorites));
        }
    }
}
