using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Models
{
    public class PropertyAddViewModel
    {
        [Display(Name ="Category")]
        public int CategoryId { get; init; }

        public IList<PropertyCategoryViewModel> Categories { get; set; }

        [Display(Name ="Image Url")]
        [Required]
        [MaxLength(1000)]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Town must be between {2} and {1} symbols!")]
        public string Town { get; init; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Quarter must be between {2} and {1} symbols!")]
        public string Quarter { get; init; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int Area { get; init; }
        
        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string Floor { get; init; }
        
        [Required]
        public int Price { get; init; }
        
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 10, 
            ErrorMessage = "Description can not be less than {2} symbols!")]
        public string Description { get; init; }
    }
}
