using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
