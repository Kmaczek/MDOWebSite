using OpenQA.Selenium;

namespace Mdo.Acceptance.Selenium.Pages
{
    public class MessagesPage : Page
    {
        public IWebElement Title
        {
            get
            {
                return this.Wait.Until(d => d.FindElement(By.XPath(@"/html/body/div[1]/div/div[2]/ui-view/div/h3")));
            }
        }

        public IWebElement Content
        {
            get
            {
                return this.Wait.Until(d => d.FindElement(By.XPath(@"/html/body/div[1]/div/div[2]/ui-view/div/p")));
            }
        }

        public MessagesPage(IWebDriver driver) : base(driver, TestHelpers.MessagesUrl)
        {
        }
    }
}
