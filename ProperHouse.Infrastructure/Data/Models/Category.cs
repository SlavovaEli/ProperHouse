using System.ComponentModel.DataAnnotations;

namespace ProperHouse.Infrastructure.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public IList<Property> Properties { get; init; } = new List<Property>();
    }
}
