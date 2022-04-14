using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models.Owner;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly ProperHouseDbContext dbContext;

        public OwnerService(ProperHouseDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void CreateOwner(Owner owner)
        {         

            dbContext.Owners.Add(owner);
            dbContext.SaveChanges();
        }

        public Owner GetOwner(int ownerId)
        {
            return dbContext.Owners
                .FirstOrDefault(o => o.Id == ownerId);
        }

        public int GetOwnerId(string userId)
        {
            return dbContext.Owners
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        public string GetOwnerName(int ownerId)
        {
            var owner = dbContext.Owners
                .FirstOrDefault(o => o.Id == ownerId);

            return owner.Name;
        }

        public string GetOwnersPhone(int ownerId)
        {
            return dbContext.Owners
                .Where(x => x.Id == ownerId)
                .Select(x => x.PhoneNumber)
                .First();
        }

        public Owner GetPropertyOwner(Property property)
        {
            return dbContext.Owners
                .Where(o => o.Id == property.OwnerId)
                .FirstOrDefault();
        }

        public bool IsUserOwner(string userId)
        {
            return dbContext
                .Owners
                .Any(o => o.UserId == userId);
                
        }

        public bool OwnerOfProperty(int ownerId, int propertyId)
        {
            return dbContext
                .Properties
                .Any(p => p.Id == propertyId && p.OwnerId == ownerId);

        }

        public int OwnerByUser(string userId)
        {
            return dbContext.Owners
                .Where(o => o.UserId == userId)
                .Select(o => o.Id)
                .FirstOrDefault();
        }

    }
}
