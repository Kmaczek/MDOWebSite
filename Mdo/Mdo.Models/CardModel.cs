//using System;
//using System.Collections.Generic;
//using System.Reflection.Emit;
//using Mdo.Models.Models;
//
//namespace Mdo.Models
//{
//    public class CardModel
//    {
//        public CardModel(
//            string name,
//            string description,
//            string flavor,
//            string rarity,
//            string imagePath,
//            string labelPath,
//            string power,
//            string thoughness,
//            string colorlessMana,
//            string loyalty,
//            int redMana,
//            int blueMana,
//            int greenMana,
//            int whiteMana,
//            int blackMana,
//            int convertedManaCost,
//            IEnumerable<string> cardTypes,
//            ExpansionModel expansion)
//        {
//            Name = name;
//            Description = description;
//            Flavor = flavor;
//            Rarity = rarity;
//            ImagePath = imagePath;
//            LabelPath = labelPath;
//            Power = power;
//            Thoughnes = thoughness;
//            RedMana = redMana;
//            BlueMana = blueMana;
//            GreenMana = greenMana;
//            WhiteMana = whiteMana;
//            BlackMana = blackMana;
//            ColorlessMana = colorlessMana;
//            Loyalty = loyalty;
//            ConvertedManaCost = convertedManaCost;
//            Expansion = expansion;
//
//            foreach (var cardType in cardTypes)
//            {
//                CardTypes.Add(new TypeModel(cardType));
//            }
//        }
//
//        private List<TypeModel> cardTypes = new List<TypeModel>();
//
//        public string Name { get; }
//
//        public ICollection<TypeModel> CardTypes => cardTypes;
//
//        public int RedMana { get; }
//        public int BlueMana { get; }
//        public int GreenMana { get; }
//        public int WhiteMana { get; }
//        public int BlackMana { get; }
//        public string ColorlessMana { get; }
//
//        public int ConvertedManaCost { get; }
//
//        public string Description { get; }
//
//        public string Flavor { get; }
//
//        public string Power { get; }
//
//        public string Thoughnes { get; }
//
//        public string Loyalty { get; }
//
//        public ExpansionModel Expansion { get; }
//
//        public string Rarity { get; }
//
//        public string ImagePath { get; }
//
//        public string LabelPath { get; }
//    }
//}
