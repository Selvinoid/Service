namespace Moodroon.DataLayer.Core.DTO
{
    using System.Collections.Generic;

    public class ConditionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Images { get; set; }    
    }
}
