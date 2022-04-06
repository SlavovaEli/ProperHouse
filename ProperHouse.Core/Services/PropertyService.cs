using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly ProperHouseDbContext dbContext;

        public PropertyService(ProperHouseDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void AddProperty(Property property)
        {           

            dbContext.Properties.Add(property);
            dbContext.SaveChanges();
                       
        }

        public IList<PropertyListingViewModel> GetAllProperties()
        {
            var properties = dbContext.Properties
                .Select(p => new PropertyListingViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                    Town = p.Town,
                    Capacity = p.Capacity,
                })
                .ToList();

            return properties;
        }
    }
}
