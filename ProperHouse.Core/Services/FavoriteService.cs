using ProperHouse.Core.Contracts;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IPropertyService propertyService;

        private readonly ProperHouseDbContext dbContext;        

        public FavoriteService(IPropertyService _propertyService,
            ProperHouseDbContext _dbContext)
        {
            propertyService = _propertyService;
            dbContext = _dbContext;
        }

        public void AddToFavorites(Favorite favorite)
        {
            if (!dbContext.Favorites.Any(f => f.UserId == favorite.UserId && f.PropertyId == favorite.PropertyId))
            {
                dbContext.Favorites.Add(favorite);
                dbContext.SaveChanges();
            }

        }

        public IList<Property> GetFavorites(string userId)
        {

            var userFavorites = dbContext.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => propertyService.GetProperty(f.PropertyId))
                .ToList();

            return userFavorites;
        }

        public bool IsUsersFavorite(string userId, int propertyId)
        {
            return dbContext.Favorites
                .Any(f => f.UserId == userId && f.PropertyId == propertyId);
        }

        public void Remove(string userId, int propertyId)
        {
            var favorite = dbContext.Favorites
                .Where(f => f.UserId == userId && f.PropertyId == propertyId)
                .FirstOrDefault();

            if (favorite != null)
            {
                dbContext.Favorites.Remove(favorite);
                dbContext.SaveChanges();
            }
        }
    }
}
