using ProperHouse.Core.Models;
using ProperHouse.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Contracts
{
    public interface IPropertyService
    {
        IList<PropertyListingViewModel> GetAllProperties();

        IList<PropertyListingViewModel> GetPublicProperties();

        int AddProperty(string userId, PropertyViewModel propertyModel);

        IList<PropertyListingViewModel> FindProperties(PropertySearchViewModel search);

        IList<string> FindAllTowns();

        IList<PropertyListingViewModel> MyProperties(string userId);

        PropertyDetailsViewModel Details(int id);

        Property GetProperty(int id);

        bool PropertyIsOwners(int id, int ownerId);

        bool Edit(int id, bool isAdmin, PropertyViewModel propertyForm);

        void Approve(int id);

    }
}
