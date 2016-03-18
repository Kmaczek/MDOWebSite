using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdo.Models.Dtos
{
    public class DetailedCardDto
    {
        public string Name { get; set; }

        public int RedMana { get; set; }
        public int BlueMana { get; set; }
        public int GreenMana { get; set; }
        public int WhiteMana { get; set; }
        public int BlackMana { get; set; }
        public string ColorlessMana { get; set; }

        public int ConvertedManaCost { get; set; }

        public string Description { get; set; }

        public string Flavor { get; set; }

        public string CreaturePower { get; set; }

        public string Thoughnes { get; set; }

        public string Rarity { get; set; }

        public string Loyalty { get; set; }

        public string ImagePath { get; set; }

        public string LabelPath { get; set; }

        public string Expansion { get; set; }

        public List<string> Tags { get; set; }

        public List<string> Types { get; set; }
    }
}
