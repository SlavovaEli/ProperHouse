using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Models
{
    public class PropertyListingViewModel
    {
        
        public int Id { get; init; }

        
        public string Category { get; set; }

        
        public string ImageUrl { get; set; }

        
        public string Town { get; set; }


        public int Capacity { get; set; }
    }
}
