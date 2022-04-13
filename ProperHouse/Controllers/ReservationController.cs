using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models.Favorite;
using ProperHouse.Core.Models.Reservation;
using ProperHouse.Infrastructure.Data.Models;
using ProperHouse.Infrastructure.Extensions;

namespace ProperHouse.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly IReservationService reservationService;

        private readonly IPropertyService propertyService;  
        
        private readonly ICategoryService categoryService;

        private readonly IOwnerService ownerService;

        public ReservationController(IReservationService _reservationService,
            IPropertyService _propertyService,
            ICategoryService _categoryService,
            IOwnerService _ownerService)
        {
            reservationService = _reservationService;
            propertyService = _propertyService;
            categoryService = _categoryService;
            ownerService = _ownerService;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(int id, ReservationViewModel reservation)
        {
            var userId = User.GetId();           

            reservation.UserId = userId;
            reservation.PropertyId = id;
            
            var property = propertyService.GetProperty(id);

            var newReservation = new Reservation
            {
                DateFrom = reservation.DateFrom,
                DateTo = reservation.DateTo,
                UserId = userId,                
                PropertyId = reservation.PropertyId,
                Property = property
            };

            reservationService.MakeReservation(newReservation);

            return RedirectToAction(nameof(MyReservations));
        }

        public IActionResult MyReservations()
        {
            var userId = User.GetId();

            var userReservations = reservationService.GetUserReservations(userId);

            foreach (var res in userReservations)
            {
                var property = propertyService.GetProperty(res.PropertyId);
                property.Category = categoryService.GetCategory(property.CategoryId);
                property.Owner = ownerService.GetOwner(property.OwnerId);
                res.Property = property;
            }

            var reservationsView = userReservations
                .Select(r => new MyReservationsViewModel
                {                    
                    ImageUrl = r.Property.ImageUrl,
                    Category = r.Property.Category.Name,
                    Town = r.Property.Town,
                    Quarter = r.Property.Quarter,
                    PhoneNumber = r.Property.Owner.PhoneNumber,
                    Price = r.Property.Price,
                    Capacity = r.Property.Capacity,
                    DateFrom = r.DateFrom,
                    DateTo = r.DateTo,
                    Owner = r.Property.Owner.Name
                })
                .ToList();

            return View(reservationsView);
        }
    }
}
