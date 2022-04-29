using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models;
using ProperHouse.Infrastructure.Data;
using ProperHouse.Infrastructure.Data.Models;

namespace ProperHouse.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ProperHouseDbContext dbContext;

        public CategoryService(ProperHouseDbContext _dbContext)
        {
            dbContext = _dbContext; 
        }

        public bool CategoryExists(int categoryId)
        {
            return dbContext.Categories.Any(c => c.Id == categoryId);
        }

        public Category GetCategory(int categoryId)
        {
            return dbContext.Categories
                .FirstOrDefault(c => c.Id == categoryId);
        }

        public string GetCategoryName(int categoryId)
        {
            var category = dbContext.Categories.FirstOrDefault(c => c.Id == categoryId);            

            if (category == null)
            {
                return string.Empty;
            }

            return category.Name;
        }

        public IList<PropertyCategoryViewModel> GetPropertyCategories()
        {
            return dbContext.Categories
                .Select(c => new PropertyCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .OrderBy(c => c.Id)
                .ToList();
        }

        
    }
}
