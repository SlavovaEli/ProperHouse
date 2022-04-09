﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Models.Owner
{
    public class CreateOwnerViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage ="Name must be between {2} and {1} symbols")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        [StringLength(20, MinimumLength =3, ErrorMessage ="Phone number must be between {2} and {1} symbols")]
        public string PhoneNumber { get; set; }
    }
}
