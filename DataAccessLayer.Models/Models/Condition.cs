namespace DataAccessLayer.Models.Models
{
    using System.Collections.Generic;

    public class Condition
    {
        public Condition()
        {
            this.Hotels = new HashSet<Hotel>();
            this.Images = new HashSet<Image>();
        }

        public int Id { get; set; }

        public string Name { get; set; }    

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
