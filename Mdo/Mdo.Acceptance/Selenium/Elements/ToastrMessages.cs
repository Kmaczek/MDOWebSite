using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Mdo.Acceptance.Selenium.Elements
{
    public class ToastrMessages
    {
        private WebDriverWait wait;
        public ReadOnlyCollection<IWebElement> Messages
        {
            get { return wait.Until(d => d.FindElements(By.XPath("//*[@id='toast-container']/div"))); }
        }

        public ToastrMessages(IWebDriver driver)
        {
            this.wait = new WebDriverWait(driver: driver, timeout: new TimeSpan(0, 0, 0, 100));
        }

    }
}
