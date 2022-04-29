using Microsoft.AspNetCore.Authorization;
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
            

            return View(userReservations);
        }      

        
        public IActionResult Cancel(int id)
        {
            var userId = User.GetId();

            reservationService.Cancel(userId, id);

            return RedirectToAction(nameof(MyReservations));
        }
    }
}
