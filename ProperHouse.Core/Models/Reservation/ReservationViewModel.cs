using System.ComponentModel.DataAnnotations;

namespace ProperHouse.Core.Models.Reservation
{
    public class ReservationViewModel
    {
        [Key]        
        public int Id { get; init; }

        [Required]
        [Display(Name ="From Date:")]
        public string DateFrom { get; set; }

        [Required]
        [Display(Name ="To Date:")]              
        public string DateTo { get; set; }

        [Required]
        public string UserId { get; set; }        

        [Required]
        public int PropertyId { get; set; }
        
    }
}
