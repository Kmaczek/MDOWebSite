using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Mdo.Common.Scraping;
using Mdo.DB.Cfg;
using Mdo.DB.Entities;
using Mdo.Models.Dtos;
using Mdo.Models.Models;
using Mdo.Persistence.Adapters;
using Mdo.Persistence.Interfaces;

namespace Mdo.Persistence.Implementations
{
    public class CardsRepository : ICardsRepository
    {
        public CardEntity GetCard(string name)
        {
            using (var context = new MdoDbContext())
            {
                var card = context.Cards
                    .Include(ex => ex.Expansion)
                    .Include(ta => ta.Tags)
                    .Include(ty => ty.Types)
                    .First(x => x.Name == name);

                return card;
            }
        }

        public IEnumerable<CardEntity> GetCards()
        {
            using (var context = new MdoDbContext())
            {
                var cards = context.Cards;

                return cards.ToList();
            }
        }

        public IEnumerable<CardDto> GetCardsToDisplay()
        {
            using (var context = new MdoDbContext())
            {
                var viewCards = context.Cards.Select(x => new CardDto
                {
                    Name = x.Name,
                    Types = x.Types.Select(t => t.Name),
                    ImagePath = x.ImagePath,
                    LabelPath = x.LabelPath
                });

                return viewCards.ToList();
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

        public void SaveScrapedCards(IEnumerable<ScrappedCardModel> scrappedCards)
        {
            using (var context = new MdoDbContext())
            {
                foreach (var scrappedCard in scrappedCards)
                {
                    if (context.Cards.Any(x => x.Name == scrappedCard.CardName))
                    {
                        continue;
                    }

                    // TODO: exp not found do not save
                    var expansion = context.Expansions.First(x => x.Name.Equals(scrappedCard.Expansion, StringComparison.InvariantCultureIgnoreCase));

                    var types = new List<TypeEntity>();
                    var tags = new List<TagEntity>();
                    foreach (var type in scrappedCard.Types)
                    {
                        if (!context.Types.Any(x => x.Name == type))
                        {
                            context.Types.Add(new TypeEntity {Name = type});
                            context.SaveChanges();
                        }
                        types.Add(context.Types.First(x => x.Name == type));
                    }

                    foreach (var tag in scrappedCard.Tags)
                    {
                        if (!context.Tags.Any(x => x.Name == tag))
                        {
                            context.Tags.Add(new TagEntity { Name = tag });
                            context.SaveChanges();
                        }
                        tags.Add(context.Tags.First(x => x.Name == tag));
                    }

                    context.Cards.Add(new CardEntity
                    {
                        Name = scrappedCard.CardName,
                        Types = types,
                        Tags = tags,
                        ImagePath = scrappedCard.ImageUrl,
                        LabelPath = scrappedCard.ImageLabel,
                        RedMana = scrappedCard.RedMana,
                        BlackMana = scrappedCard.BlackMana,
                        BlueMana = scrappedCard.BlueMana,
                        Expansion = expansion,
                        ColorlessMana = scrappedCard.ColorlessMana,
                        ConvertedManaCost = scrappedCard.ConvertedManaCost,
                        Loyalty = scrappedCard.Loyalty,
                        Description = scrappedCard.CardText,
                        Flavor = scrappedCard.FlavorText,
                        GreenMana = scrappedCard.GreenMana,
                        CreaturePower = scrappedCard.Power,
                        Rarity = scrappedCard.Rarity,
                        Thoughnes = scrappedCard.Thoughness,
                        WhiteMana = scrappedCard.WhiteMana
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
