namespace DataAccessLayer.Models.Models
{
    using System.Collections.Generic;

    public class Image
    {
        public Image()
        {
            this.Conditionses = new HashSet<Condition>();
            this.Hotels = new HashSet<Hotel>();
            this.Conditionses = new HashSet<Condition>();
        }
        public int Id { get; set; }

        public string Url { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }

        public virtual ICollection<Condition> Conditionses { get; set; }
    }
}
