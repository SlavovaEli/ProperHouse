using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProperHouse.Infrastructure.Data.Models
{
    public class Property
    {
        [Key]
        public int Id { get; init; }

        public bool IsPublic { get; set; }

        [Required]        
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ImageUrl { get; set; }    
        
        [Required]
        [MaxLength(50)]
        public string Town { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Quarter { get; set; }

        [Required]        
        public int Area { get; set; }

        [Required]
        [MaxLength(10)]
        public string Floor { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public int Price { get; set; }        

        [Required]        
        public string Description { get; set; }

        public IList<Reservation> Reservations { get; set; } = new List<Reservation>();

        public int OwnerId { get; set; }

        public Owner Owner { get; set; }

        
    }
}
