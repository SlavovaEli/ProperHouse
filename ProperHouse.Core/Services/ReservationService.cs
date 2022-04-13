using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models.Reservation;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ProperHouseDbContext dbContext;

        private readonly IPropertyService propertyService;

        public ReservationService(ProperHouseDbContext _dbContext,
            IPropertyService _propertyService)
        {
            dbContext = _dbContext;
            propertyService = _propertyService;
        }

        public IList<Reservation> GetUserReservations(string id)
        {
            return dbContext.Reservations
                .Where(r => r.UserId == id)
                .ToList();
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
