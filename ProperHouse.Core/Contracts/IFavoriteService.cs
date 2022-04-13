﻿using ProperHouse.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Contracts
{
    public interface IFavoriteService
    {
        void AddToFavorites(Favorite favorite);

        IList<Property> GetFavorites(string userId);
    }
}
