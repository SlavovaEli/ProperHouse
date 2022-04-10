using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(50)]
        public string? FullName { get; set; }

        public IList<Reservation> Reservations { get; set; }   
    }
}
