using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperHouse.Core.Models.Reservation
{
    public class ReservationViewModel
    {
        [Key]        
        public int Id { get; init; }

        [Required]
        [Display(Name ="From Date:")]        
        [StringLength(30, MinimumLength = 10, ErrorMessage ="Please use the asked date format")]
        [RegularExpression(@"/(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)
                            (?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)
                            0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|
                            (?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])
                            (\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})/",
                       ErrorMessage ="Please use provided date format")]           
        public string DateFrom { get; set; }

        [Required]
        [Display(Name ="To Date:")]        
        [StringLength(30, MinimumLength = 10, ErrorMessage = "Please use the asked date format")]
        [RegularExpression(@"/(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)
                            (?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)
                            0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|
                            (?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])
                            (\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})/",
                       ErrorMessage = "Please use provided date format")]
        public string DateTo { get; set; }

        [Required]
        public string UserId { get; set; }        

        [Required]
        public int PropertyId { get; set; }
        
    }
}
