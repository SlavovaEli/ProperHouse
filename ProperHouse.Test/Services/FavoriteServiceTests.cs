using ProperHouse.Core.Contracts;
using ProperHouse.Core.Services;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;
using ProperHouse.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProperHouse.Test.Services
{
    public class FavoriteServiceTests : IDisposable
    {
        private const string firstFavoriteUserId = "FirstFavoriteUserId";
        private const string secondFavoriteUserId = "SecondFavoriteUserId";
        private const int firstFavoritePropertyId = 1;
        private const int secondFavoritePropertyId = 2;

        private readonly ProperHouseDbContext dbContext;
        private readonly IPropertyService propertyService;
        private readonly IFavoriteService favoriteService;

        public FavoriteServiceTests()
        {
            dbContext = DatabaseMock.Instance;
            propertyService = new PropertyService(dbContext, null, null);
            favoriteService = new FavoriteService(propertyService, dbContext);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Fact]        
        public void AddToFavoritesShouldAddFavoriteToDatabaseWhenItDoesNotExist()
        {
            //Arrange
            var firstFavorite = new Favorite
            {
                UserId = firstFavoriteUserId,
                PropertyId = firstFavoritePropertyId
            };

            var secondFavorite = new Favorite
            {
                UserId = secondFavoriteUserId,
                PropertyId = secondFavoritePropertyId
            };

            favoriteService.AddToFavorites(firstFavorite);
            favoriteService.AddToFavorites(secondFavorite);

            //Act
            var result = dbContext.Favorites.Count();

            //Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void AddToFavoritesShouldNotAddFavoriteToDatabaseWhenItExists()
        {
            //Arrange
            var firstFavorite = new Favorite
            {
                UserId = firstFavoriteUserId,
                PropertyId = firstFavoritePropertyId
            };

            var secondFavorite = new Favorite
            {
                UserId = firstFavoriteUserId,
                PropertyId = firstFavoritePropertyId
            };

            favoriteService.AddToFavorites(firstFavorite);
            favoriteService.AddToFavorites(secondFavorite);

            //Act
            var result = dbContext.Favorites.Count();

            //Assert
            Assert.Equal(1, result);
        }

        [Fact]        
        public void GetFavoritesShouldReturnAllUserFavorites()
        {
            //Arrange
            var firstFavorite = new Favorite
            {
                UserId = firstFavoriteUserId,
                PropertyId = firstFavoritePropertyId
            };

            var secondFavorite = new Favorite
            {
                UserId = firstFavoriteUserId,
                PropertyId = secondFavoritePropertyId
            };

            favoriteService.AddToFavorites(firstFavorite);
            favoriteService.AddToFavorites(secondFavorite);

            //Act
            var result = favoriteService.GetFavorites(firstFavoriteUserId);

            //Assert
            Assert.Equal(2, result.Count);
            Assert.IsType<List<Property>>(result);
        }

        [Fact]
        public void IsUsersFavoriteShouldBeTrueIfPropertyIsUsersFavorite()
        {
            //Arrange
            var firstFavorite = new Favorite
            {
                UserId = firstFavoriteUserId,
                PropertyId = firstFavoritePropertyId
            };

            var secondFavorite = new Favorite
            {
                UserId = secondFavoriteUserId,
                PropertyId = secondFavoritePropertyId
            };

            favoriteService.AddToFavorites(firstFavorite);
            favoriteService.AddToFavorites(secondFavorite);

            //Act
            var result = favoriteService.IsUsersFavorite(firstFavoriteUserId, firstFavoritePropertyId);
            var secondResult = favoriteService.IsUsersFavorite(secondFavoriteUserId, secondFavoritePropertyId);

            //Assert
            Assert.True(result);
            Assert.True(secondResult);
        }

        [Fact]
        public void IsUsersFavoriteShouldBeFalseIfPropertyIsNotUsersFavorite()
        {
            //Arrange
            var firstFavorite = new Favorite
            {
                UserId = firstFavoriteUserId,
                PropertyId = firstFavoritePropertyId
            };

            var secondFavorite = new Favorite
            {
                UserId = secondFavoriteUserId,
                PropertyId = secondFavoritePropertyId
            };

            favoriteService.AddToFavorites(firstFavorite);
            favoriteService.AddToFavorites(secondFavorite);

            //Act
            var result = favoriteService.IsUsersFavorite(firstFavoriteUserId, secondFavoritePropertyId);
            var secondResult = favoriteService.IsUsersFavorite(secondFavoriteUserId, firstFavoritePropertyId);

            //Assert
            Assert.False(result);
            Assert.False(secondResult);
        }

        [Fact]
        public void RemoveShouldDeleteFavoriteWhenItExists()
        {
            //Arrange
            var firstFavorite = new Favorite
            {
                UserId = firstFavoriteUserId,
                PropertyId = firstFavoritePropertyId
            };            

            favoriteService.AddToFavorites(firstFavorite);
            favoriteService.Remove(firstFavoriteUserId, firstFavoritePropertyId);

            //Act
            var result = dbContext.Favorites.Count();

            //Assert
            Assert.Equal(0, result);            
        }

        [Fact]
        public void RemoveShouldNotDeleteFavoriteWhenItDoesNotExist()
        {
            //Arrange
            var firstFavorite = new Favorite
            {
                UserId = firstFavoriteUserId,
                PropertyId = firstFavoritePropertyId
            };

            favoriteService.AddToFavorites(firstFavorite);
            favoriteService.Remove(firstFavoriteUserId, secondFavoritePropertyId);

            //Act
            var result = dbContext.Favorites.Count();

            //Assert
            Assert.Equal(1, result);
        }
    }
}
