using ProperHouse.Infrastructure.Data.Models;

namespace ProperHouse.Core.Contracts
{
    public interface IOwnerService
    {
        bool IsUserOwner(string userId);

        void CreateOwner(Owner owner);

        int GetOwnerId(string userId);

        string GetOwnerName(int userId);

        string GetOwnersPhone(int ownerId);

        Owner GetPropertyOwner(Property property);

        Owner GetOwner(int ownerId);

        bool OwnerOfProperty(int ownerId, int propertyId);

        int OwnerByUser(string userId);

    }
}
