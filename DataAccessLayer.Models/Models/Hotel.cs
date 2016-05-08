namespace DataAccessLayer.Models.Models
{
    using System.Collections.Generic;

    public class Hotel
    {
        public Hotel()
        {
            this.Images = new HashSet<Image>();
            this.Orders = new HashSet<Order>();
            this.Rooms = new HashSet<Room>();
            this.Conditionses = new HashSet<Condition>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public int Stars { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public virtual ICollection<Condition> Conditionses { get; set; }
    }
}
