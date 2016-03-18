using System.Collections.Generic;
using Mdo.Common.Scraping;
using Mdo.DB.Entities;
using Mdo.Models;
using Mdo.Models.Dtos;
using Mdo.Models.Models;

namespace Mdo.Persistence.Interfaces
{
    public interface ICardsRepository
    {
        CardEntity GetCard(string name);
        IEnumerable<CardEntity> GetCards();
        IEnumerable<CardDto> GetCardsToDisplay();
        IEnumerable<ExpansionModel> GetExpansions();

        void SaveScrapedCards(IEnumerable<ScrappedCardModel> scrappedCards);
    }
}
