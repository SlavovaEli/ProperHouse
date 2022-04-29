namespace ProperHouse.Core.Models
{
    public class PropertyDetailsViewModel
    {
        public int Id { get; set; }

        public string Category { get; set; }
                
        public string ImageUrl { get; set; }
                
        public string Town { get; set; }
                
        public string Quarter { get; set; }
                
        public int Area { get; set; }
                
        public string Floor { get; set; }
                
        public int Capacity { get; set; }
                
        public int Price { get; set; }
                
        public string Description { get; set; }

        public string Owner { get; set; }

        public int OwnerId { get; set; }

        public string PhoneNumber { get; set; }
    }
}
