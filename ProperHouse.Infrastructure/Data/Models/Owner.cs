using System.ComponentModel.DataAnnotations;

namespace ProperHouse.Infrastructure.Data.Models
{
    public class Owner
    {
        public Owner()
        {
            Properties = new List<Property>();            
        }

        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public IList<Property> Properties { get; set; }        

        [Required]
        public string UserId { get; set; }
    }
}
