using ProperHouse.Core.Contracts;
using ProperHouse.Core.Services;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;
using ProperHouse.Test.Mocks;
using System;
using System.Linq;
using Xunit;

namespace ProperHouse.Test.Services
{
    public class OwnerServiceTests : IDisposable
    {
        private const string OwnerName = "TestName";
        private const string UserId = "TestUserId";
        private const string OwnerPhone = "+359 111111111";

        private readonly ProperHouseDbContext dbContext;
        private readonly IOwnerService ownerService;

        public OwnerServiceTests()
        {
            dbContext = DatabaseMock.Instance;
            ownerService = new OwnerService(dbContext);
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Fact]
        public void CreateOwnerShouldAddOwnerToDb()
        {
            //Arrange
            var owner = AddOwner();
            ownerService.CreateOwner(owner);

            //Act
            var result = dbContext.Owners.Count();

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetOwnerShouldReturnRightTypeAndOwner()
        {
            //Arrange
            var owner = AddOwner();
            
            dbContext.Owners.Add(owner);
            dbContext.SaveChanges();

            //Act
            var result = ownerService.GetOwner(1);

            //Assert
            Assert.IsType<Owner>(result);
            Assert.Equal(OwnerName, result.Name);
            Assert.Equal(UserId, result.UserId);
        }

        [Fact]
        public void GetOwnerIdShouldReturnRightTypeAndId()
        {
            //Arrange
            var owner = AddOwner();

            dbContext.Owners.Add(owner);
            dbContext.SaveChanges();

            //Act
            var result = ownerService.GetOwnerId(UserId);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(1, result);            
        }

        [Fact]
        public void GetOwnerNameShouldReturnRightTypeAndName()
        {
            //Arrange
            var owner = AddOwner();

            dbContext.Owners.Add(owner);
            dbContext.SaveChanges();

            //Act
            var result = ownerService.GetOwnerName(1);

            //Assert
            Assert.IsType<string>(result);
            Assert.Equal(OwnerName, result);
        }

        [Fact]
        public void GetOwnersPhoneShouldReturnRightTypeAndValue()
        {
            //Arrange
            var owner = AddOwner();

            dbContext.Owners.Add(owner);
            dbContext.SaveChanges();

            //Act
            var result = ownerService.GetOwnersPhone(1);

            //Assert
            Assert.IsType<string>(result);
            Assert.Equal(OwnerPhone, result);
        }

        [Fact]
        public void GetProperyOwnerShouldReturnRightTypeAndValue()
        {
            //Arrange
            var owner = AddOwner();

            var property = AddProperty();
            
            dbContext.Owners.Add(owner);
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();

            //Act
            var result = ownerService.GetPropertyOwner(property);

            //Assert
            Assert.IsType<Owner>(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void IsUserOwnerShouldReturnTrueWhenUserIsOwner()
        {
            //Arrange
            var owner = AddOwner();           

            dbContext.Owners.Add(owner);            
            dbContext.SaveChanges();

            //Act
            var result = ownerService.IsUserOwner(UserId);

            //Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public void IsUserOwnerShouldReturnFalseWhenUserIsNotOwner()
        {
            //Arrange
            var owner = AddOwner();

            dbContext.Owners.Add(owner);
            dbContext.SaveChanges();

            //Act
            var result = ownerService.IsUserOwner("AnotherUserId");

            //Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public void OwnerOfPropertyShouldReturnTrueWhenOwnersProperty()
        {
            //Arrange
            var owner = AddOwner();

            var property = AddProperty();

            dbContext.Owners.Add(owner);
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();

            //Act
            var result = ownerService.OwnerOfProperty(1, 1);

            //Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public void OwnerOfPropertyShouldReturnFalseWhenNotOwnersProperty()
        {
            //Arrange
            var owner = AddOwner();

            var property = AddProperty();

            dbContext.Owners.Add(owner);
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();

            //Act
            var result = ownerService.OwnerOfProperty(1, 2);

            //Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        private static Owner AddOwner()
        {
            return new Owner
            {
                Name = OwnerName,
                PhoneNumber = OwnerPhone,
                UserId = UserId
            };
        }

        private static Property AddProperty()
        {
            var property = new Property
            {
                Description = "Some test description",
                Town = "TestTown",
                Quarter = "TestQuarter",
                Capacity = 2,
                Area = 100,
                OwnerId = 1,
                Floor = "5",
                ImageUrl = "TestImage"
            };

            return property;
        }
    }
}
