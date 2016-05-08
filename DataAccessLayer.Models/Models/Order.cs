namespace DataAccessLayer.Models.Models
{
    using System;

    public class Order
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdHotel { get; set; }

        public int IdRoom { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime LeaveDate { get; set; }

        public float TotalPrice { get; set; }

        public int CountOfDays { get; set; }

        public virtual User User { get; set; }

        public virtual Hotel Hotel { get; set; }

        public virtual Room Room { get; set; }  
    }
}
