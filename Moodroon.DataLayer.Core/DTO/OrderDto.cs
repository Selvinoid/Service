namespace Moodroon.DataLayer.Core.DTO
{
    using System;

    public class OrderDto
    {
        public int Id { get; set; }

        public string HotelName { get; set; }

        public string UserName { get; set; }

        public int RoomNumber { get; set; }

        public int CountOfDays { get; set; }    

        public DateTime ArrivalDate { get; set; }

        public DateTime LeaveDate { get; set; }

        public float TotalPrice { get; set; }
    }

    public class UiOrderDto 
    {
        public int Id { get; set; }

        public string HotelName { get; set; }

        public string UserName { get; set; }

        public int RoomNumber { get; set; }

        public int CountOfDays { get; set; }

        public string ArrivalDate { get; set; }

        public string LeaveDate { get; set; }

        public float TotalPrice { get; set; }
    }
}
