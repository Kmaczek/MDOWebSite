using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mdo.DB.Cfg;
using Mdo.Models;
using Mdo.Models.Models;
using Mdo.Persistence.Adapters;
using Mdo.Persistence.Interfaces;

namespace Mdo.Persistence.Implementations
{
    public class CardsRepository : ICardsRepository
    {
        public IEnumerable<CardModel> GetCards()
        {
            using (var context = new MdoDbContext())
            {
                var cards = context.Cards;
                var list = new List<CardModel>();
                foreach (var card in cards)
                {
                    list.Add(new CardAdapter(card).CardModel);
                }

                return list;
            }
        }

        public IEnumerable<ExpansionModel> GetExpansions()
        {
            using (var context = new MdoDbContext())
            {
                var expansions = context.Expansions;
                var list = new List<ExpansionModel>();
                foreach (var expansion in expansions)
                {
                    list.Add(new ExpansionAdapter(expansion).ExpansionModel);
                }
                return list;
            }
        }
    }
}
