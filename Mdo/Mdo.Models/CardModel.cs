using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdo.Models
{
    public class CardModel
    {
        public CardModel(
            string name,
            string spellType,
            string cardType,
            string description,
            string flavor,
            string rarity,
            string imagePath,
            string power,
            string thoughness,
            int redMana,
            int blueMana,
            int greenMana,
            int whiteMana,
            int blackMana,
            int colorlessMana)
        {
            Name = name;
            SpellType = spellType;
            CardType = cardType;
            Description = description;
            Flavor = flavor;
            Rarity = rarity;
            ImagePath = imagePath;
            Power = power;
            Thoughnes = thoughness;
            RedMana = redMana;
            BlueMana = blueMana;
            GreenMana = greenMana;
            WhiteMana = whiteMana;
            BlackMana = blackMana;
            ColorlessMana = colorlessMana;
        }

        public string Name { get; set; }

        public string SpellType { get; set; }

        public string CardType { get; set; }

        public int RedMana { get; set; }
        public int BlueMana { get; set; }
        public int GreenMana { get; set; }
        public int WhiteMana { get; set; }
        public int BlackMana { get; set; }
        public int ColorlessMana { get; set; }

        public string Description { get; set; }

        public string Flavor { get; set; }

        public string Power { get; set; }

        public string Thoughnes { get; set; }

//        public Expansion Expansion { get; set; }

        public string Rarity { get; set; }

        public string ImagePath { get; set; }
    }
}
