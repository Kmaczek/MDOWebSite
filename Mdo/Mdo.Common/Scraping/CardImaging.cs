using System;
using System.Drawing;
using System.IO;
using System.Net;

namespace Mdo.Common.Scraping
{
    public class CardImaging
    {
        public static void ExtractLabel(string existingImageUri, string destinationLabelUri, int yoffset = 0)
        {
            if (!File.Exists(existingImageUri))
            {
                throw new ArgumentException("existingImageUri need to point to valid image on disk");
            }
            
            Rectangle crop = new Rectangle(11, 13 + yoffset, 201, 22);
            Image img = Image.FromFile(existingImageUri);
            Bitmap bmpImage = new Bitmap(img);
            var toSave = bmpImage.Clone(crop, bmpImage.PixelFormat);
            toSave.Save(destinationLabelUri);
        }

        public static string FetchImage(string url, string cardName)
        {
            var savePath = Path.Combine(Environment.CurrentDirectory, "mdo_images", "cards", cardName + ".png");
            if (!File.Exists(savePath))
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, savePath);

                return savePath;
            }

            return string.Empty;
        }
    }
}
