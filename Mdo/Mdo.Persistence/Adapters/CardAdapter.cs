//using System.Collections.Generic;
//using System.Linq;
//using Mdo.DB.Entities;
//using Mdo.Models;
//using Mdo.Models.Models;
//
//namespace Mdo.Persistence.Adapters
//{
//    public class CardAdapter
//    {
//        private CardModel _cardModel;
//        private CardEntity _cardEntity;
//
//        public CardAdapter(CardModel cardModel)
//        {
//            _cardModel = cardModel;
//            _cardEntity = CreateEntity();
//        }
//
//        public CardAdapter(CardEntity cardEntity)
//        {
//            _cardEntity = cardEntity;
//            _cardModel = CreateModel();
//        }
//
//        public CardModel CardModel
//        {
//            get { return _cardModel; }
//        }
//
//        public CardEntity CardEntity
//        {
//            get { return _cardEntity; }
//        }
//
//        private CardModel CreateModel()
//        {
//            if (_cardEntity == null)
//            {
//                return null;
//            }
//
//            var cardModel = new CardModel(
//                _cardEntity.Name,
//                _cardEntity.Description,
//                _cardEntity.Flavor,
//                _cardEntity.Rarity,
//                _cardEntity.ImagePath,
//                _cardEntity.LabelPath,
//                _cardEntity.CreaturePower,
//                _cardEntity.Thoughnes,
//                _cardEntity.ColorlessMana,
//                _cardEntity.Loyalty,
//                _cardEntity.RedMana,
//                _cardEntity.BlueMana,
//                _cardEntity.GreenMana,
//                _cardEntity.WhiteMana,
//                _cardEntity.BlackMana,
//                _cardEntity.ConvertedManaCost,
//                _cardEntity.Types.Select(x => x.Name),
//                new ExpansionAdapter(_cardEntity.Expansion).ExpansionModel);
//
//            return cardModel;
//        }
//
//        private CardEntity CreateEntity()
//        {
//            if (_cardModel == null)
//            {
//                return null;
//            }
//
//            var cardEntity = new CardEntity()
//            {
//                Name = _cardModel.Name,
//                Types = _cardModel.CardTypes.Select(x => new TypeEntity {Name = x.Name}) as ICollection<TypeEntity>,
//                Description = _cardModel.Description,
//                Flavor = _cardModel.Flavor,
//                Rarity = _cardModel.Rarity,
//                ImagePath = _cardModel.ImagePath,
//                LabelPath = _cardModel.LabelPath,
//                CreaturePower = _cardModel.Power,
//                Thoughnes = _cardModel.Thoughnes,
//                RedMana = _cardModel.RedMana,
//                BlueMana = _cardModel.BlueMana,
//                GreenMana = _cardModel.GreenMana,
//                WhiteMana = _cardModel.WhiteMana,
//                BlackMana = _cardModel.BlackMana,
//                ColorlessMana = _cardModel.ColorlessMana,
//                // TODO: this expansion conversion seems incorrect, investigate
//                Expansion = new ExpansionAdapter(_cardModel.Expansion).ExpansionEntity,
//                Loyalty = _cardModel.Loyalty
//            };
//
//            return cardEntity;
//        }
//    }
//}
