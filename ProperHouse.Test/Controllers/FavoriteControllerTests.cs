using ProperHouse.Controllers;
using ProperHouse.Core.Contracts;
using ProperHouse.Core.Services;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProperHouse.Test.Controllers
{
    public class FavoriteControllerTests : IDisposable
    {
        private readonly ProperHouseDbContext dbContext;
        private readonly ICategoryService categoryService;
        private readonly IPropertyService propertyService;
        private readonly IFavoriteService favoriteService;
        private readonly FavoriteController favoriteController;

        public FavoriteControllerTests()
        {
            dbContext = DatabaseMock.Instance;
            categoryService = new CategoryService(dbContext);
            propertyService = new PropertyService(dbContext, categoryService, null);
            favoriteService = new FavoriteService(propertyService, dbContext);
            favoriteController = new FavoriteController(propertyService, favoriteService, categoryService);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Fact]
        public void AddFavoriteShouldReturnView()
        {

        }
    }
}
