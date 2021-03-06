namespace ProperHouse.Core.Models.Favorite
{
    public class MyReservationsViewModel
    {
        public int ReservationId { get; set; }

        public int PropertyId { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public string Town { get; set; }

        public string Quarter { get; set; }     
               
        public int Capacity { get; set; }

        public int Price { get; set; }        

        public string Owner { get; set; }

        public string PhoneNumber { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }
        
    }
}
