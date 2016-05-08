namespace Moodroon.DataLayer.Core.DTO
{
    using System.Collections.Generic;

    public class RoomDto
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int PersonCount { get; set; }

        public int Price { get; set; }  

        public string HotelName { get; set; }   

        public string Description { get; set; }

        public List<string> Images { get; set; }    
    }
}
