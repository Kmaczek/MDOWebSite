using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using NLog;

namespace Mdo.Common.Scraping
{
    public class PageScrapper
    {
        private readonly PageFetcher pageFetcher = new PageFetcher();
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public int Total { get; set; }

        public IEnumerable<ScrappedCardModel> ScrapCards()
        {
            var mainPage = pageFetcher.GetMainPage();
            var cardNames = ScrapCardNamesAndPaths(mainPage);
            var cardList = new List<ScrappedCardModel>();
            foreach (var cardName in cardNames)
            {
                var cardPage = pageFetcher.GetCardPage(cardName);
                var scrap = ScrapCard(cardPage);
                logger.Debug($"Scrapped [{cardList.Count}/{Total}]: {scrap}");
                cardList.Add(scrap);
            }

            return cardList;
        }

        public IEnumerable<ScrappedCardModel> ScrapCards(IEnumerable<string> exclude)
        {
            var mainPage = pageFetcher.GetMainPage();
            var cardNames = ScrapCardNamesAndPaths(mainPage); 
            var cardList = new List<ScrappedCardModel>();
            foreach (var cardName in cardNames)
            {
                if (exclude.Any(x => x == cardName.Item1))
                {
                    continue;
                }
                var cardPage = pageFetcher.GetCardPage(cardName);
                var scrap = ScrapCard(cardPage);
                logger.Debug($"Scrapped [{cardList.Count}/{Total}]: {scrap}");
                cardList.Add(scrap);

//                if (cardList.Count > 5)
//                    break;
            }

            return cardList;
        }


        //TODO: remove this nasty tuple
        private IEnumerable<Tuple<string, string>> ScrapCardNamesAndPaths(string page)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(page);
            var table = document.DocumentNode.SelectNodes(@"//*[@id='mw-content-text']/table[2]/tr").Skip(1).ToArray();
            Total = int.Parse(document.DocumentNode.SelectSingleNode(@"//*[@id='mw-content-text']/table[2]/tr[1]/th").InnerText.Split(' ')[0]);
            var namesAndPaths = new List<Tuple<string, string>>();
            foreach (var child in table)
            {
                var path = child.ChildNodes[2].ChildNodes[0].Attributes[0].Value;
                var name = child.ChildNodes[2].ChildNodes[0].InnerText.Trim();
                namesAndPaths.Add(new Tuple<string, string>(name, path));
            }

            return namesAndPaths;
        }

        private ScrappedCardModel ScrapCard(string page)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(page);
            var scrappedCard = new ScrappedCardModel();

            var table = document.DocumentNode.SelectNodes("//*[@id='mw-content-text']/table/tr");
            ExtractDataFromTable(scrappedCard, table);

            SetImageData(document, scrappedCard);
            AddTags(document, scrappedCard);

            return scrappedCard;
        }

        private void ExtractDataFromTable(ScrappedCardModel scrappedCard, HtmlNodeCollection table)
        {
            SetName(scrappedCard, table);
            SetManaCost(scrappedCard, table);
            SetConvertedManaCost(scrappedCard, table);
            SetTypes(scrappedCard, table);
            SetCardText(scrappedCard, table);
            SetFlavorText(scrappedCard, table);
            SetPowerAndThoughness(scrappedCard, table);
            SetExpansion(scrappedCard, table);
            SetRarity(scrappedCard, table);
            SetLoyalty(scrappedCard, table);
        }

        private void SetImageData(HtmlDocument document, ScrappedCardModel scrappedCard)
        {
            var image = document.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/p/img") ??
                        document.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/img");

            scrappedCard.ImageUrl = image.GetAttributeValue("data-src", null) ?? image.GetAttributeValue("src", string.Empty);
            scrappedCard.ImageName = image.GetAttributeValue("data-image-key", string.Empty);
        }

        private static void SetName(ScrappedCardModel scrappedCard, HtmlNodeCollection table)
        {
            var row = table.FirstOrDefault(x => x.ChildNodes[1].InnerText.Contains("Card name"));
            if (row != null) scrappedCard.CardName = row.ChildNodes[2].InnerText.Trim();
        }

        private static void SetManaCost(ScrappedCardModel scrappedCard, HtmlNodeCollection table)
        {
            var row = table.FirstOrDefault(x => x.ChildNodes[1].InnerText.Contains("Mana Cost"));
            if (row != null)
            {
                foreach (var childNode in row.ChildNodes[2].ChildNodes)
                {
                    if (childNode.Name == "#text")
                        continue;

                    var alt = childNode.GetAttributeValue("alt", "");
                    if (alt.Contains("CMC"))
                    {
                        if (alt.Length > 4)
                        {
                            scrappedCard.ColorlessMana = alt[3].ToString() + alt[4];
                        }
                        else
                        {
                            scrappedCard.ColorlessMana = alt[3].ToString();
                        }
                    }
                    else if (alt.Contains("Color R"))
                    {
                        scrappedCard.RedMana++;
                    }
                    else if (alt.Contains("Color U"))
                    {
                        scrappedCard.BlueMana++;
                    }
                    else if (alt.Contains("Color G"))
                    {
                        scrappedCard.GreenMana++;
                    }
                    else if (alt.Contains("Color W"))
                    {
                        scrappedCard.WhiteMana++;
                    }
                    else if (alt.Contains("Color B"))
                    {
                        scrappedCard.BlackMana++;
                    }
                }
            }
        }

        private static void SetConvertedManaCost(ScrappedCardModel scrappedCard, HtmlNodeCollection table)
        {
            var row = table.FirstOrDefault(x => x.ChildNodes[1].InnerText.Contains("Converted Mana Cost"));
            if (row != null) scrappedCard.ConvertedManaCost = int.Parse(row.ChildNodes[2].InnerText.Trim());
        }

        private static void SetTypes(ScrappedCardModel scrappedCard, HtmlNodeCollection table)
        {
            var row = table.FirstOrDefault(x => x.ChildNodes[1].InnerText.Contains("Types"));
            if (row != null) scrappedCard.Types = row.ChildNodes[2].InnerText.Trim().Replace(" —", "").Split(' ').ToList();
        }

        private static void SetCardText(ScrappedCardModel scrappedCard, HtmlNodeCollection table)
        {
            var row = table.FirstOrDefault(x => x.ChildNodes[1].InnerText.Contains("Card Text"));
            if (row != null) scrappedCard.CardText = row.ChildNodes[2].InnerText.Trim();
        }

        private static void SetFlavorText(ScrappedCardModel scrappedCard, HtmlNodeCollection table)
        {
            var row = table.FirstOrDefault(x => x.ChildNodes[1].InnerText.Contains("Flavor Text"));
            if (row != null) scrappedCard.FlavorText = row.ChildNodes[2].InnerText.Trim();
        }

        private static void SetPowerAndThoughness(ScrappedCardModel scrappedCard, HtmlNodeCollection table)
        {
            var row = table.FirstOrDefault(x => x.ChildNodes[1].InnerText.Contains("P/T"));
            if (row != null)
            {
                var pt = row.ChildNodes[2].InnerText.Trim().Trim('(', ')').Split('/');
                scrappedCard.Power = pt[0];
                scrappedCard.Thoughness = pt[1];
            }
        }

        private static void SetExpansion(ScrappedCardModel scrappedCard, HtmlNodeCollection table)
        {
            var row = table.FirstOrDefault(x => x.ChildNodes[1].InnerText.Contains("Expansion"));
            if (row != null) scrappedCard.Expansion = row.ChildNodes[2].InnerText.Trim();
        }

        private static void SetRarity(ScrappedCardModel scrappedCard, HtmlNodeCollection table)
        {
            var row = table.FirstOrDefault(x => x.ChildNodes[1].InnerText.Contains("Rarity"));
            if (row != null) scrappedCard.Rarity = row.ChildNodes[2].InnerText.Trim();
        }

        private static void SetLoyalty(ScrappedCardModel scrappedCard, HtmlNodeCollection table)
        {
            var row = table.FirstOrDefault(x => x.ChildNodes[1].InnerText.Contains("Loyalty"));
            if (row != null) scrappedCard.Loyalty = row.ChildNodes[2].InnerText.Trim();
        }

        private static void AddTags(HtmlDocument document, ScrappedCardModel scrappedCard)
        {
            var tags = document.DocumentNode.SelectSingleNode("//*[@id='articleCategories']/div[1]/ul");
            var filteredTags = tags.ChildNodes.Where(x => !string.IsNullOrWhiteSpace(x.InnerText));

            foreach (var tag in filteredTags)
            {
                var trimmedText = tag.InnerText.Trim();
                if (trimmedText == "Cards" || trimmedText == "Add category")
                {
                    continue;
                }
                scrappedCard.Tags.Add(trimmedText);
            }
        }
    }
}
