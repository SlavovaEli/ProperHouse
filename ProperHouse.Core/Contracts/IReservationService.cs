using ProperHouse.Core.Models.Favorite;
using ProperHouse.Infrastructure.Data.Models;

namespace ProperHouse.Core.Contracts
{
    public interface IReservationService
    {
        void MakeReservation(Reservation reservation);

        IList<MyReservationsViewModel> GetUserReservations(string id);

        MyReservationsViewModel GetReservation(int id);

        void Cancel(string userId, int reservationId);

    }
}
