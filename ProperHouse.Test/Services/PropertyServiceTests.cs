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

        [Fact]
        public void EditShouldReturnTrueAndEditPropertyWhenPropertyIsNotNull()
        {
            //Arrange
            var property = AddProperty();
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();
            var propertyModel = AddPropertyViewModel();
            var isAdmin = true;

            //Act
            var result = propertyService.Edit(1, isAdmin, propertyModel);
            var editedProperty = propertyService.GetProperty(1);
            var isPublic = editedProperty.IsPublic;

            //Assert
            Assert.True(result);
            Assert.True(isPublic);
        }

        [Fact]
        public void EditShouldReturnFalseWhenPropertyIsNull()
        {
            //Arrange
            var property = AddProperty();
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();
            var propertyModel = AddPropertyViewModel();
            var isAdmin = true;

            //Act
            var result = propertyService.Edit(3, isAdmin, propertyModel);
            var editedProperty = propertyService.GetProperty(1);
            var isPublic = editedProperty.IsPublic;

            //Assert
            Assert.False(result);            
        }

        [Fact]
        public void FindAllTownsShouldReturnDistinctTownsAndOrderThem()
        {   
            //Arrange
            var propertiesToAdd = new List<Property>();

            for(int i = 0; i < 4; i++)
            {
                var property = new Property
                {
                    Id = i + 1,
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
                    IsPublic = true
                };
                propertiesToAdd.Add(property);
            }
            propertiesToAdd[0].Town = "Sofia";
            propertiesToAdd[1].Town = "Sofia";
            propertiesToAdd[2].Town = "Varna";
            propertiesToAdd[3].Town = "Burgas";

            dbContext.Properties.AddRange(propertiesToAdd);
            dbContext.SaveChanges();

            //Act
            var towns = propertyService.FindAllTowns();

            //Assert
            Assert.Equal(3, towns.Count);
            Assert.Equal("Burgas", towns[0]);
            Assert.Equal("Sofia", towns[1]);
            Assert.Equal("Varna", towns[2]);
        }

        [Fact]
        public void GetPublicPropertiesShouldReturnRightTypeAndOnlyPublicProperties()
        {
            //Arrange
            var property = AddProperty();
            var category = AddCategory();
            dbContext.Categories.Add(category);
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();

            //Act
            var properties = propertyService.GetPublicProperties();

            //Assert
            Assert.Equal(0, properties.Count);
            Assert.IsType<List<PropertyListingViewModel>>(properties);
            
        }

        [Fact]
        public void GetPublicPropertiesShouldReturnRightTypeAndPublicProperties()
        {
            //Arrange
            var property = AddProperty();
            property.IsPublic = true;
            var category = AddCategory();
            dbContext.Categories.Add(category);
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();

            //Act
            var properties = propertyService.GetPublicProperties();

            //Assert
            Assert.Equal(1, properties.Count);
            Assert.IsType<List<PropertyListingViewModel>>(properties);
            
        }

        [Fact]
        public void GetAllPropertiesShouldReturnAllProperties()
        {
            //Arrange
            var propertiesToAdd = new List<Property>();

            for (int i = 0; i < 4; i++)
            {
                var property = new Property
                {
                    Id = i + 1,
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
                    IsPublic = true
                };
                propertiesToAdd.Add(property);
            }
            
            dbContext.Properties.AddRange(propertiesToAdd);
            dbContext.SaveChanges();

            //Act
            var propertyToFind = propertyService.GetProperty(2);
            var propertyId = propertyToFind.Id;

            //Assert
            Assert.Equal(2, propertyId);
            Assert.IsType<Property>(propertyToFind);            
        }

        [Fact]
        public void GetPropertyShouldReturnRightProperty()
        {
            //Arrange
            var propertiesToAdd = new List<Property>();

            for (int i = 0; i < 4; i++)
            {
                var property = new Property
                {
                    Id = i + 1,
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
                    IsPublic = true
                };
                propertiesToAdd.Add(property);
            }
            var category = AddCategory();
            dbContext.Categories.Add(category);
            dbContext.Properties.AddRange(propertiesToAdd);
            dbContext.SaveChanges();

            //Act
            var properties = propertyService.GetAllProperties();
            var firstPropertyId = properties[0].Id;

            //Assert
            Assert.Equal(4, properties.Count);
            Assert.IsType<List<PropertyListingViewModel>>(properties);
            Assert.Equal(4, firstPropertyId);
        }

        [Fact]
        public void MyPropertiesShouldReturnOnlyOwnersProperties()
        {
            //Arrange
            var category = AddCategory();            
            var owner = new Owner
            {
                Name = "TestName",
                UserId = "TestUserId",
                PhoneNumber = "TestPhoneNumber"
            };
            var property = AddProperty();
            property.Owner = owner;
            property.Category = category;
            dbContext.Categories.Add(category);
            dbContext.Owners.Add(owner);
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();

            //Act
            var result = propertyService.MyProperties("TestUserId");

            //Assert
            Assert.IsType<List<PropertyListingViewModel>>(result);
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void PropertyIsOwnersShouldReturnTrueWhenIsOwners()
        {
            //Arrange
            var property = AddProperty();
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();

            //Act
            var result = propertyService.PropertyIsOwners(1, 1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void PropertyIsOwnersShouldReturnFalseWhenIsOwners()
        {
            //Arrange
            var property = AddProperty();
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();

            //Act
            var result = propertyService.PropertyIsOwners(1, 2);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ApproveShouldChangeIsPublic()
        {
            //Arrange
            var property = AddProperty();
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();

            //Act
            propertyService.Approve(1);
            var editedProperty = propertyService.GetProperty(1);

            //Assert
            Assert.True(editedProperty.IsPublic);
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
