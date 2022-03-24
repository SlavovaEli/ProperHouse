using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
