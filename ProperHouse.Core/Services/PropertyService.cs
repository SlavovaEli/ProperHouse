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

        private readonly IOwnerService ownerService;

        public PropertyService(ProperHouseDbContext _dbContext,
            ICategoryService _categoryService,
            IOwnerService _ownerService)
        {
            dbContext = _dbContext;
            categoryService = _categoryService;
            ownerService = _ownerService;
        }

        public void AddProperty(Property property)
        {           

            dbContext.Properties.Add(property);
            dbContext.SaveChanges();
                       
        }

        public PropertyDetailsViewModel Details(int id)
        {
            var property = dbContext.Properties
                .Where(p => p.Id == id)
                .FirstOrDefault();

            return new PropertyDetailsViewModel
            {
                Id = property.Id,
                ImageUrl = property.ImageUrl,
                Category = categoryService.GetCategoryName(property.CategoryId),
                Capacity = property.Capacity,
                Description = property.Description,
                Area = property.Area,
                Floor = property.Floor,
                Owner = ownerService.GetOwnerName(property.OwnerId),
                OwnerId = property.OwnerId,
                Price = property.Price,
                Quarter = property.Quarter,
                Town = property.Town,
                PhoneNumber = ownerService.GetOwnersPhone(property.OwnerId)
            };
        }

        public bool Edit(int id, PropertyViewModel propertyForm)
        {
            var propertyToEdit = dbContext.Properties
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if(propertyToEdit == null)
            {
                return false;
            }
            
            propertyToEdit.ImageUrl = propertyForm.ImageUrl;
            propertyToEdit.CategoryId = propertyForm.CategoryId;
            propertyToEdit.Area = propertyForm.Area;
            propertyToEdit.Quarter = propertyForm.Quarter;
            propertyToEdit.Town = propertyForm.Town;
            propertyToEdit.Price = propertyForm.Price;
            propertyToEdit.Capacity = propertyForm.Capacity;
            propertyToEdit.Description = propertyForm.Description;
            propertyToEdit.Floor = propertyForm.Floor;

            dbContext.SaveChanges();

            return true;
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
                    Category = categoryService.GetCategoryName(p.CategoryId),
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

        public Property GetProperty(int id)
        {
            return dbContext.Properties
                .FirstOrDefault(p => p.Id == id);
        }

        public IList<PropertyListingViewModel> MyProperties(string userId)
        {
            return dbContext.Properties
                .Where(p => p.Owner.UserId == userId)
                .Select(p => new PropertyListingViewModel
                {
                    Id=p.Id,
                    ImageUrl=p.ImageUrl,
                    Category=p.Category.Name,
                    Capacity = p.Capacity,
                    Town=p.Town
                })
                .ToList();
        }

        public bool PropertyIsOwners(int id, int ownerId)
        {
            var property = this.GetProperty(id);

            return property.OwnerId == ownerId;
        }

    }
}
