namespace DataAccessLayer.Models.Models
{
    using System.Collections.Generic;

    public class Room
    {
        public Room()
        {
            this.Images = new HashSet<Image>();
            this.Orders = new HashSet<Order>();
        }
        public int Id { get; set; }

        public int Number { get; set; }

        public int PersonCount { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }  

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public int IdHotel { get; set; }

        public Hotel Hotel { get; set; }
    }
}
