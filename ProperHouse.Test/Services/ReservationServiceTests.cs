using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models.Favorite;
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
    public class ReservationServiceTests : IDisposable
    {
        private readonly ProperHouseDbContext dbContext;
        private readonly ICategoryService categoryService;
        private readonly IOwnerService ownerService;
        private readonly IPropertyService propertyService;
        private readonly IReservationService reservationService;

        public ReservationServiceTests()
        {
            dbContext = DatabaseMock.Instance;
            categoryService = new CategoryService(dbContext);
            ownerService = new OwnerService(dbContext);
            propertyService = new PropertyService(dbContext, categoryService, ownerService);
            reservationService = new ReservationService(dbContext, propertyService, 
                                                             categoryService, ownerService);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Fact]
        public void CancelShouldDeleteReservation()
        {
            //Arrange
            var user = AddUser();
            var property = AddProperty();

            var reservation = new Reservation
            {
                PropertyId = 1,
                DateFrom = "22/12/2022",
                DateTo = "23/12/2022",
                UserId = "TestUserId",
            };

            var secondReservation = new Reservation
            {
                PropertyId = 1,
                DateFrom = "22/10/2022",
                DateTo = "23/10/2022",
                UserId = "TestUserId",
            };
            user.Reservations.Add(reservation);
            user.Reservations.Add(secondReservation);
            property.Reservations.Add(reservation);
            property.Reservations.Add(secondReservation);
            dbContext.Users.Add(user);
            dbContext.Properties.Add(property); 
            dbContext.Reservations.Add(reservation);
            dbContext.Reservations.Add(secondReservation);         
            
            dbContext.SaveChanges();

            //Act
            reservationService.Cancel("TestUserId", 1);
            var result = dbContext.Reservations.Count();
            var userReservations = user.Reservations.Count();
            var propertyReservations = property.Reservations.Count();

            //Assert
            Assert.Equal(1, result);
            Assert.Equal(1, propertyReservations);
            Assert.Equal(1, userReservations);
        }

        [Fact]
        public void GetReservationShouldReturnProperType()
        {
            var category = new Category
            {
                Id = 1,
                Name = "TestCategory"
            };
            dbContext.Categories.Add(category);

            var property = AddProperty();
            property.Category = category;
            property.CategoryId = 1;            

            var owner = new Owner
            {                
                Name = "TestName",
                PhoneNumber = "TestNumber",
                UserId = "TestUserId"

            };
            dbContext.Owners.Add(owner);
            property.Owner = owner;
            property.OwnerId = owner.Id;
            dbContext.Properties.Add(property);

            var reservation = new Reservation
            {                
                PropertyId = 1,
                DateFrom = "22/12/2022",
                DateTo = "23/12/2022",
                UserId = "TestUserId",
                Property = property
            };

            dbContext.Reservations.Add(reservation);
            dbContext.SaveChanges();

            //Act
            var result = reservationService.GetReservation(1);

            //Assert
            Assert.IsType<MyReservationsViewModel>(result);
            Assert.Equal(1, result.PropertyId);
        }

        [Fact]
        public void GetUserReservationShouldReturnProperType()
        {
            var category = new Category
            {
                Id = 1,
                Name = "TestCategory"
            };
            dbContext.Categories.Add(category);

            var property = AddProperty();
            property.Category = category;
            property.CategoryId = 1;

            var owner = new Owner
            {
                Name = "TestName",
                PhoneNumber = "TestNumber",
                UserId = "TestUserId"

            };
            dbContext.Owners.Add(owner);
            property.Owner = owner;
            property.OwnerId = owner.Id;
            dbContext.Properties.Add(property);

            var reservation = new Reservation
            {
                PropertyId = 1,
                DateFrom = "22/12/2022",
                DateTo = "23/12/2022",
                UserId = "TestUserId",
                Property = property
            };

            dbContext.Reservations.Add(reservation);
            dbContext.SaveChanges();

            //Act
            var result = reservationService.GetUserReservations("TestUserId");

            //Assert
            Assert.IsType<List<MyReservationsViewModel>>(result);
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void MakeReservationShouldAddReservation()
        {
            //Arrange
            var user = AddUser();
            var property = AddProperty();
            dbContext.Users.Add(user);
            dbContext.Properties.Add(property);
            dbContext.SaveChanges();

            var reservation = new Reservation
            {                
                DateFrom = "22/12/2022",
                DateTo = "23/12/2022",
                UserId = user.Id,
                PropertyId = property.Id,
                Property = property
            };
            //Act
            reservationService.MakeReservation(reservation);

            var result = dbContext.Reservations.Count();
            var userReservations = user.Reservations.Count();
            var propertyReservations = property.Reservations.Count();

            //Assert
            Assert.Equal(1, result);
            Assert.Equal(1, userReservations);
            Assert.Equal(1, propertyReservations);
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
                ImageUrl = "TestImage",
                Price = 100,
                Reservations = new List<Reservation>()
            };

            return property;
        }
        private static User AddUser()
        {
            return new User
            {
                UserName = "TestUsername",
                Email = "test@test.com",
                Id = "TestUserId",
                Reservations = new List<Reservation>()
            };
        }
    }
}
