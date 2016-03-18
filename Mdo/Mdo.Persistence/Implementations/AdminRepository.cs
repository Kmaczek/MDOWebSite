using System;
using System.IO;
using System.Linq;
using Mdo.Common;
using Mdo.Common.Scraping;
using Mdo.DB.Cfg;
using Mdo.Persistence.Interfaces;

namespace Mdo.Persistence.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        public void ProcessImageLabel(string cardName, string existingImageUri, string destinationLabelUri, int yoffset)
        {
            using (var context = new MdoDbContext())
            {
                var card = context.Cards.First(x => x.Name == cardName);
                if (!string.IsNullOrEmpty(card.ImagePath))
                {
                    CardImaging.ExtractLabel(existingImageUri, destinationLabelUri, yoffset);
                }
            }
        }

        public void FetchCardImage(string cardName, string url)
        {
            var savePath = CardImaging.FetchImage(url, cardName);

            if (string.IsNullOrEmpty(savePath))
            {
                return;
            }

            using (var context = new MdoDbContext())
            {
                var card = context.Cards.First(x => x.Name == cardName);
                if (card != null)
                {
                    card.ImagePath = savePath;
                    context.SaveChanges();
                }
            }
        }
    }
}
