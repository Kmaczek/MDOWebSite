using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mdo.Persistence.Interfaces;
using Nancy;
using NLog;

namespace Mdo.WebApi.Modules
{
    public class CardsModule : NancyModule
    {
        private readonly ICardsRepository cardsRepository;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public CardsModule(ICardsRepository cardsRepository) : base("/cards")
        {
            this.cardsRepository = cardsRepository;

            GetAllCards();
            GetAllExpansions();
        }

        public void GetAllCards()
        {
            Get["/"] = o =>
            {
                try
                {
                    logger.Info("GET /cards invoked");
                    return Response.AsJson(cardsRepository.GetCards());
                }
                catch (Exception e)
                {
                    logger.Error(e, "Cannot fetch cards. Server error {0}", e.Message);
                    return Response.AsJson(new { Message = "Cannot fetch cards. Server error." }, HttpStatusCode.InternalServerError);
                }
            };
        }

        public void GetAllExpansions()
        {
            Get["/expansions"] = o =>
            {
                try
                {
                    logger.Info("GET /cards/expansions invoked");
                    return Response.AsJson(cardsRepository.GetExpansions());
                }
                catch (Exception e)
                {
                    logger.Error(e, "Cannot fetch expansions. Server error {0}", e.Message);
                    return Response.AsJson(new { Message = "Cannot fetch cards. Server error." }, HttpStatusCode.InternalServerError);
                }
            };
        }
    }
}
