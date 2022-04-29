using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models.Favorite;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;

namespace ProperHouse.Core.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ProperHouseDbContext dbContext;

        private readonly IPropertyService propertyService;

        private readonly ICategoryService categoryService;

        private readonly IOwnerService ownerService;

        public ReservationService(ProperHouseDbContext _dbContext,
            IPropertyService _propertyService,
            ICategoryService _categoryService,
            IOwnerService _ownerService)
        {
            dbContext = _dbContext;
            propertyService = _propertyService;
            categoryService = _categoryService;
            ownerService = _ownerService;
        }

        public void Cancel(string userId, int reservationId)
        {
            var reservation = dbContext.Reservations.Find(reservationId);
            var user = dbContext.Users.Find(userId);
            var property = dbContext.Properties.Find(reservation.PropertyId);
            
            user.Reservations.Remove(reservation);
            property.Reservations.Remove(reservation);
            dbContext.Reservations.Remove(reservation);
            dbContext.SaveChanges();            

        }

        public MyReservationsViewModel GetReservation(int id)
        {
            var reservation = dbContext.Reservations.Find(id);

            var property = propertyService.GetProperty(reservation.PropertyId);
            property.Category = categoryService.GetCategory(property.CategoryId);
            property.Owner = ownerService.GetOwner(property.OwnerId);
            reservation.Property = property;

            var reservationView = new MyReservationsViewModel
            {
                ReservationId = reservation.Id,
                PropertyId = reservation.PropertyId,
                ImageUrl = reservation.Property.ImageUrl,
                Category = reservation.Property.Category.Name,
                Town = reservation.Property.Town,
                Quarter = reservation.Property.Quarter,
                PhoneNumber = reservation.Property.Owner.PhoneNumber,
                Price = reservation.Property.Price,
                Capacity = reservation.Property.Capacity,
                DateFrom = reservation.DateFrom,
                DateTo = reservation.DateTo,
                Owner = reservation.Property.Owner.Name
            };

            return reservationView;
        }

        public IList<MyReservationsViewModel> GetUserReservations(string id)
        {
            List<MyReservationsViewModel> userReservationsViews = new List<MyReservationsViewModel>();   

            var userReservations =  dbContext.Reservations
                .Where(r => r.UserId == id)
                .ToList();

            foreach (var res in userReservations)
            {
                userReservationsViews.Add(GetReservation(res.Id));
            }            

            return userReservationsViews;
        }

        public void MakeReservation(Reservation reservation)
        {
            var user = dbContext.Users
                .Where(u => u.Id == reservation.UserId)
                .FirstOrDefault();

            var property = dbContext.Properties
                .Where(p => p.Id == reservation.PropertyId)
                .FirstOrDefault();

            user.Reservations.Add(reservation);
            property.Reservations.Add(reservation);

            dbContext.Reservations.Add(reservation);            
            dbContext.SaveChanges();
        }
    }
}
