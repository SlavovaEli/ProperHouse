﻿using ProperHouse.Core.Models.Owner;
using ProperHouse.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Contracts
{
    public interface IOwnerService
    {
        bool IsUserOwner(string userId);

        void CreateOwner(Owner owner);

        int GetOwnerId(string userId);

    }
}
