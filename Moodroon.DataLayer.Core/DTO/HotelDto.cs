namespace Moodroon.DataLayer.Core.DTO
{
    using System.Collections.Generic;

    public class HotelDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Stars { get; set; }

        public string Adress { get; set; }

        public string Description { get; set; }

        public List<string> Images { get; set; }

        public List<ConditionDto> Conditions { get; set; }

        public List<RoomDto> Rooms { get; set; }
    }
}
