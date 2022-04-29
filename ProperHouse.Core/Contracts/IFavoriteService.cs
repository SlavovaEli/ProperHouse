using ProperHouse.Infrastructure.Data.Models;

namespace ProperHouse.Core.Contracts
{
    public interface IFavoriteService
    {
        void AddToFavorites(Favorite favorite);

        IList<Property> GetFavorites(string userId);

        bool IsUsersFavorite(string userId, int propertyId);

        void Remove(string userId, int propertyId);
    }
}
