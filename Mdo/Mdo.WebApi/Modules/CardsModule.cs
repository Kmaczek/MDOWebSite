using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Mdo.Common;
using Mdo.Common.Scraping;
using Mdo.Models.Dtos;
using Mdo.Persistence.Interfaces;
using Mdo.WebApi.Massage;
using Mdo.WebApi.Setup;
using Nancy;
using NLog;
using HttpStatusCode = Nancy.HttpStatusCode;

namespace Mdo.WebApi.Modules
{
    public class CardsModule : NancyModule
    {
        private readonly ICardsRepository cardsRepository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CardsModule(ICardsRepository cardsRepository) : base("/cards")
        {
            this.cardsRepository = cardsRepository;

            GetCards();
            GetAllExpansions();
            ScrapCards();
        }

        public void GetCards()
        {
            Get["/"] = o =>
            {
                try
                {
                    logger.Info("GET /cards invoked");
                    var cards = cardsRepository.GetCardsToDisplay().ToList();
                    PrepareCard.WebPaths(cards);

                    return Response.AsJson(cards);
                }
                catch (Exception e)
                {
                    logger.Error(e, "Cannot fetch cards. Server error {0}", e.Message);
                    return Response.AsJson(new { Message = "Cannot fetch cards. Server error." },
                        HttpStatusCode.InternalServerError);
                }
            };

            Get["/{cardname}"] = o =>
            {
                try
                {
                    logger.Info($"GET /cards/{o.cardname} invoked");
                    var card = cardsRepository.GetCard((string)o.cardname);
                    PrepareCard.WebPaths(card);
                    var detailedCard = new DetailedCardDto
                    {
                        Name = card.Name,
                        BlackMana = card.BlackMana,
                        BlueMana = card.BlueMana,
                        ColorlessMana = card.ColorlessMana,
                        ConvertedManaCost = card.ConvertedManaCost,
                        CreaturePower = card.CreaturePower,
                        Description = card.Description,
                        Expansion = card.Expansion.Name,
                        Flavor = card.Flavor,
                        GreenMana = card.GreenMana,
                        ImagePath = card.ImagePath,
                        LabelPath = card.LabelPath,
                        Loyalty = card.Loyalty,
                        Rarity = card.Rarity,
                        RedMana = card.RedMana,
                        Thoughnes = card.Thoughnes,
                        WhiteMana = card.WhiteMana
                    };
                    detailedCard.Types = card.Types.Select(x => x.Name).ToList();
                    detailedCard.Tags = card.Tags.Select(x => x.Name).ToList();

                    return Response.AsJson(detailedCard);
                }
                catch (Exception e)
                {
                    logger.Error(e, "Cannot fetch card. Server error {0}", e.Message);
                    return Response.AsJson(new { Message = "Cannot fetch cards. Server error." },
                        HttpStatusCode.InternalServerError);
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
                    return Response.AsJson(new { Message = "Cannot fetch cards. Server error." },
                        HttpStatusCode.InternalServerError);
                }
            };
        }

        //TODO: move this into admin
        public void ScrapCards()
        {
            Get["/scrap"] = o =>
            {
                try
                {
                    logger.Info("GET /scrap invoked");
                    var exclude = cardsRepository.GetCards().Select(x => x.Name).ToArray();
                    var cards = new PageScrapper().ScrapCards(exclude).ToList();
                    SaveImage(cards);
                    cardsRepository.SaveScrapedCards(cards);
                    return Response.AsJson(new { Message = $"Scrapped and saved {cards.Count} cards" });
                }
                catch (Exception e)
                {
                    logger.Error(e, "Scrapping failed. Message: {0}", e.Message);
                    return Response.AsJson(new { Message = "Scrapping failed. Server error." },
                        HttpStatusCode.InternalServerError);
                }
            };
        }

        private void SaveImage(IEnumerable<ScrappedCardModel> cardModels)
        {
            var extension = ".png";
            foreach (var cardModel in cardModels)
            {
                cardModel.ImageLabel = $"label_{cardModel.CardName.Replace(' ', '_')}";
                var imageSavePath = Path.Combine(ApplicationInfo.Instance.CardSavePath, cardModel.CardName + extension);
                var labelSavePath = Path.Combine(ApplicationInfo.Instance.LabelsPath, cardModel.ImageLabel + extension);

                if (!File.Exists(imageSavePath))
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(cardModel.ImageUrl, imageSavePath);
                    cardModel.ImageUrl = cardModel.CardName + extension;
                }

                if (!File.Exists(labelSavePath))
                {
                    CardImaging.ExtractLabel(imageSavePath, labelSavePath);
                    cardModel.ImageLabel = cardModel.ImageLabel + extension;
                }
            }
        }
    }
}
