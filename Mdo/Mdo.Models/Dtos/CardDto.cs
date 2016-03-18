using System.Collections.Generic;

namespace Mdo.Models.Dtos
{
    public class CardDto
    {
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string LabelPath { get; set; }

        public IEnumerable<string> Types { get; set; }
    }
}
