using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Infrastructure.Data.Models
{
    public class Property
    {
        [Key]
        public int Id { get; init; }

        [Required]        
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; init; }

        [Required]
        [MaxLength(1000)]
        public string ImageUrl { get; set; }    
        
        [Required]
        [StringLength(50)]
        public string Town { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Quarter { get; set; }

        [Required]        
        public int Area { get; set; }

        [Required]
        [StringLength(10)]
        public string Floor { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]        
        public string Description { get; set; }
    }
}
