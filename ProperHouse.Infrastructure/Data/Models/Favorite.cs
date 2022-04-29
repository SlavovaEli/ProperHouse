using System.ComponentModel.DataAnnotations;

namespace ProperHouse.Infrastructure.Data.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int PropertyId { get; set; }
    }
}
