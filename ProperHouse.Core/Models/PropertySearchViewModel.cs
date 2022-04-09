using ProperHouse.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Models
{
    public class PropertySearchViewModel
    {
        [Display(Name ="Choose Category")]
        public int CategoryId { get; set; }

        public IList<PropertyCategoryViewModel> Categories { get; set; }

        [Display(Name = "Choose Town")]
        public string Town { get; set; }
        
        public IList<string> Towns { get; set; }

        [Range(1, 100000)]
        [Display(Name = "Maximum Price")]
        public int Price { get; set; }

        [Range(1, 3000)]
        [Display(Name = "Minimum Area")]
        public int Area { get; set; }

        [Range(1, 30)]
        [Display(Name = "Minimum number of guests")]
        public int Capacity { get; set; }
    }
}
