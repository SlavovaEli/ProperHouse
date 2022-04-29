using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models;
using ProperHouse.Core.Services;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;
using ProperHouse.Test.Mocks;
using System;
using System.Collections.Generic;
using Xunit;

namespace ProperHouse.Test.Services
{
    public class CategoryServiceTests : IDisposable
    {
        private const string CategoryName = "Hotel";
        private const string SecondCategoryName = "Hotel2";

        private readonly ProperHouseDbContext dbContext;
        private readonly ICategoryService categoryService;

        public CategoryServiceTests()
        {
            dbContext = DatabaseMock.Instance;
            categoryService = new CategoryService(dbContext);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Fact]
        public void CategoryexistsShouldReturnTrueWhenValidId()
        {
            //Arrange
            dbContext.Categories.Add(new Category { Name = CategoryName, Id = 1});
            dbContext.SaveChanges();

            //Act
            var result = categoryService.CategoryExists(1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void CategoryexistsShouldReturnFalseWhenInvalidId()
        {
            //Arrange
            dbContext.Categories.Add(new Category { Name = CategoryName, Id = 1 });            
            dbContext.SaveChanges();

            //Act
            var result = categoryService.CategoryExists(8);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GetCategoryShouldReturnTypeCategoryAndTheRightObject()
        {
            //Arrange
            dbContext.Categories.Add(new Category { Name = CategoryName, Id = 1 });
            dbContext.Categories.Add(new Category { Name = SecondCategoryName, Id = 2 });
            dbContext.SaveChanges();

            //Act
            var result = categoryService.GetCategory(1);

            //Assert
            Assert.IsType<Category>(result);
            Assert.Equal(CategoryName, result.Name);
        }


        [Fact]
        public void GetCategoryNameShouldReturnCategoryNameAndTheRightObject()
        {
            //Arrange
            dbContext.Categories.Add(new Category { Name = CategoryName, Id = 1 });
            dbContext.Categories.Add(new Category { Name = SecondCategoryName, Id = 2 });
            dbContext.SaveChanges();

            //Act
            var result = categoryService.GetCategoryName(2);

            //Assert            
            Assert.Equal(SecondCategoryName, result);
        }

        [Fact]
        public void GetPropertyCategoriesShouldReturnListOfpropertyCategoryViewModel()
        {
            //Arrange
            dbContext.Categories.Add(new Category { Name = CategoryName, Id = 1 });
            dbContext.Categories.Add(new Category { Name = SecondCategoryName, Id = 2 });
            dbContext.SaveChanges();

            //Act
            var result = categoryService.GetPropertyCategories();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<PropertyCategoryViewModel>>(result);
            Assert.Equal(2, result.Count);
            
        }
    }
}
