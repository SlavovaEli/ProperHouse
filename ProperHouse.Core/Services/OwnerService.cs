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

        public bool IsUserOwner(string userId)
        {
            return dbContext
                .Owners
                .Any(p => p.UserId == userId);
        }



        
    }
}
