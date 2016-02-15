using System.Collections.Generic;
using Mdo.Models;
using Mdo.Models.Models;

namespace Mdo.Persistence.Interfaces
{
    public interface ICardsRepository
    {
        IEnumerable<CardModel> GetCards();
        IEnumerable<ExpansionModel> GetExpansions();
    }
}
