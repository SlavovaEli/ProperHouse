using ProperHouse.Core.Models;
using ProperHouse.Infrastructure.Data.Models;

namespace ProperHouse.Core.Contracts
{
    public interface ICategoryService
    {
        IList<PropertyCategoryViewModel> GetPropertyCategories();

        bool CategoryExists(int categoryId);

        string GetCategoryName(int categoryId);

        Category GetCategory(int categoryId);
        
    }
}
