using System;
using System.Net;
using System.Text;

namespace Mdo.Common.Scraping
{
    public class PageFetcher
    {
        private Uri mainPage = new Uri("http://magicduels.wikia.com/wiki/Cards");
        private string cardPage = @"http://magicduels.wikia.com{0}";

        public PageFetcher()
        {
            
        }

        public string GetMainPage()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = new UTF8Encoding();
                return client.DownloadString(mainPage);
            }
        }

        public string GetCardPage(Tuple<string, string> nameAndPath)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = new UTF8Encoding();
                return client.DownloadString(CreateCardUri(nameAndPath.Item2));
            }
        }

        private Uri CreateCardUri(string uriCardName)
        {
            return new Uri(String.Format(cardPage, uriCardName));
        }
    }
}
