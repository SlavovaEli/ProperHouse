using ProperHouse.Core.Models.Reservation;
using ProperHouse.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Contracts
{
    public interface IReservationService
    {
        void MakeReservation(Reservation reservation);

        IList<Reservation> GetUserReservations(string id);
        
    }
}
