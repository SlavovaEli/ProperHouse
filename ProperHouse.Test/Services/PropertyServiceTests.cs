using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models;
using ProperHouse.Core.Services;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;
using ProperHouse.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProperHouse.Test.Services
{
    public class PropertyServiceTests : IDisposable
    {
        private readonly ProperHouseDbContext dbContext;
        private readonly ICategoryService categoryService;
        private readonly IOwnerService ownerService;
        private readonly IPropertyService propertyService;

        public PropertyServiceTests()
        {
            dbContext = DatabaseMock.Instance;
            categoryService = new CategoryService(dbContext);
            ownerService = new OwnerService(dbContext);
            propertyService = new PropertyService(dbContext, categoryService, ownerService);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Fact]
        public void AddPropertyShouldAddPropertyInDb()
        {
            //Arrange
            var category = AddCategory();
            var owner = new Owner
            {
                Name = "TestName",
                UserId = "TestUserId",
                PhoneNumber = "TestPhoneNumber"
            };
            dbContext.Categories.Add(category);
            dbContext.Owners.Add(owner);
            var propertyModel = AddPropertyViewModel();

            //Act
            var result = propertyService.AddProperty("TestUserId", propertyModel);

            //Assert
            Assert.Equal(1,result);
            Assert.Equal(1, dbContext.Properties.Count());
        }

        [Fact]
        public void DetailsShouldReturnCorrectType()
        {
            //Arrange
            var category = AddCategory();
            var owner = new Owner
            {
                Id = 1,
                Name = "TestName",
                UserId = "TestUserId",
                PhoneNumber = "TestPhoneNumber"
            };
            dbContext.Categories.Add(category);
            dbContext.Owners.Add(owner);
            var property = AddProperty();
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();

            //Act
            var result = propertyService.Details(1);

            //Assert
            Assert.IsType<PropertyDetailsViewModel>(result);
        }

        private static Category AddCategory()
        {
            return new Category
            {
                Name = "TestCategoryName",
                Id = 1
            };
        }

        private static PropertyViewModel AddPropertyViewModel()
        {
            return new PropertyViewModel
            {

                CategoryId = 1,                
                Town = "TestTown",
                Quarter = "TestQuarter",
                Capacity = 2,
                Area = 100,
                Floor = "TestFloor",
                Price = 100,
                Description = "TestDescription",
                ImageUrl = "TestUrl"             
                
            };
        }

        private Property AddProperty()
        {
            return new Property
            {
                Id = 1,
                CategoryId = 1,                
                Town = "TestTown",
                Quarter = "TestQuarter",
                Capacity = 2,
                Area = 100,
                Floor = "TestFloor",
                Price = 100,
                Description = "TestDescription",
                ImageUrl = "TestUrl",
                OwnerId = 1,                
                IsPublic = false
            };
        }
    }
}
