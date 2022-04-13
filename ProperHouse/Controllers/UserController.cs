using Microsoft.AspNetCore.Mvc;
using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models;
using ProperHouse.Infrastructure.Extensions;

namespace ProperHouse.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }


        /*public ActionResult Favorites()
        {
            var userId = this.User.GetId();

            var properties = userService.GetFavorites(userId);
            var formProperties = properties
                .Select(p => new PropertyListingViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Town = p.Town,
                    Capacity = p.Capacity,
                    Category = p.Category.Name
                })
                .ToList();

            return View(formProperties);
            
        }

        [HttpPost]
        public IActionResult AddFavorites(int id)
        {
            var userId = this.User.GetId();

            bool added = userService.AddToFavorites(id, userId);

            if (!added)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(UserController.Favorites), "User");
        }*/
    }
}
