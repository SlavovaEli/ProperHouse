using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models.Owner;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;
using ProperHouse.Infrastructure.Extensions;

namespace ProperHouse.Controllers
{
    public class OwnerController : Controller
    {
        private readonly IOwnerService ownerService;

        public OwnerController(IOwnerService _ownerService)
        {
            ownerService = _ownerService;
        }
                   

        [Authorize]
        public IActionResult Create() => View();

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateOwnerViewModel owner)
        {
            var userId = this.User.GetId();

            if(ownerService.IsUserOwner(userId))
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return View(owner);
            }

            var newOwner = new Owner()
            {
                Name = owner.Name,
                PhoneNumber = owner.PhoneNumber,
                UserId = userId
            };

            ownerService.CreateOwner(newOwner);

            return RedirectToAction("All", "Property");
        }
        
    }
}
