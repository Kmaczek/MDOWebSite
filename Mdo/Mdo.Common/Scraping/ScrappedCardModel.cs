using System.Collections.Generic;
using System.Text;

namespace Mdo.Common.Scraping
{
    public class ScrappedCardModel
    {
        public ScrappedCardModel()
        {
            Tags = new List<string>();
        }

        public string CardName { get; set; }

        public int RedMana { get; set; }
        public int BlueMana { get; set; }
        public int GreenMana { get; set; }
        public int WhiteMana { get; set; }
        public int BlackMana { get; set; }
        public string ColorlessMana { get; set; }

        public int ConvertedManaCost { get; set; }

        public List<string> Types { get; set; }

        public string CardText { get; set; }

        public string FlavorText { get; set; }

        public string Power { get; set; }

        public string Thoughness { get; set; }

        public string Expansion { get; set; }

        public string Rarity { get; set; }

        public string Loyalty { get; set; }

        public string ImageUrl { get; set; }

        public string ImageLabel { get; set; }

        public string ImageName { get; set; }

        public List<string> Tags { get; set; }

        public override string ToString()
        {
            return new StringBuilder(200)
                .Append(CardName)
                .Append(" - Mana: ")
                .Append(ConvertedManaCost)
                .Append(" - [P/T: ")
                .Append(Power)
                .Append("/")
                .Append(Thoughness)
                .Append("] - ")
                .Append(Expansion)
                .ToString();
        }
    }
}
