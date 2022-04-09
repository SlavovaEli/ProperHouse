using ProperHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Contracts
{
    public interface ICategoryService
    {
        IList<PropertyCategoryViewModel> GetPropertyCategories();

        bool CategoryExists(int categoryId);

        string GetCategoryName(int categoryId);
        
    }
}
