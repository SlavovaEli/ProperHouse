using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Models.Favorite
{
    public class FavoriteViewModel
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public string Quarter { get; set; }

        public string Town { get; set; }

        public int Capacity { get; set; }

        public int Price { get; set; }

        public string UserId { get; set; }
    }
}
