using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProperHouse.Infrastructure.Data.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string DateFrom { get; set; }

        [Required]
        public string DateTo { get; set; }
        
        [Required]
        public string UserId { get; set; }     
               
        [Required]
        public int PropertyId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public Property Property { get; set; }        

    }
}
