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

        private readonly ICategoryService categoryService;

        public PropertyService(ProperHouseDbContext _dbContext,
            ICategoryService _categoryService)
        {
            dbContext = _dbContext;
            categoryService = _categoryService;
        }

        public void AddProperty(Property property)
        {           

            dbContext.Properties.Add(property);
            dbContext.SaveChanges();
                       
        }

        public IList<string> FindAllTowns()
        {
            return dbContext.Properties
                .OrderBy(p => p.Town)
                .Select(p => p.Town)                
                .Distinct()                
                .ToList();
        }

        public IList<PropertyListingViewModel> FindProperties(PropertySearchViewModel search)
        {
            var propertiesQuery = dbContext.Properties.ToList();

            if (search.Town != "All")
            {
                propertiesQuery = propertiesQuery
                    .Where(p => p.Town == search.Town)
                    .ToList();
            }

            if (search.CategoryId != 0)
            {
                propertiesQuery = propertiesQuery
                    .Where(p => p.CategoryId == search.CategoryId)
                    .ToList();
            }            

            if(search.Capacity != 0)
            {
                propertiesQuery = propertiesQuery
                    .Where(p => p.Capacity >= search.Capacity)
                    .ToList();
            }

            if(search.Price != 0)
            {
                propertiesQuery = propertiesQuery
                    .Where(p => p.Price <= search.Price)
                    .ToList();
            }

            if (search.Area != 0)
            {
                propertiesQuery = propertiesQuery
                    .Where(p => p.Area >= search.Area)
                    .ToList();
            }

            List<PropertyListingViewModel> foundProperties = propertiesQuery
                .Select(p => new PropertyListingViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Capacity = p.Capacity,
                    Town = p.Town,
                    Category = categoryService.GetCategoryName(search.CategoryId),
                })
                .OrderByDescending(p => p.Id)
                .ToList();

            return foundProperties;
                
        }

        public IList<PropertyListingViewModel> GetAllProperties()
        {
            return dbContext.Properties
                .Select(p => new PropertyListingViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                    Town = p.Town,
                    Capacity = p.Capacity,
                })
                .OrderByDescending(p => p.Id)
                .ToList();

        }        
    }
}
