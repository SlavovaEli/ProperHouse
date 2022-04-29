using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProperHouse.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {       

        [MaxLength(50)]
        public string? FullName { get; set; }

        public IList<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
