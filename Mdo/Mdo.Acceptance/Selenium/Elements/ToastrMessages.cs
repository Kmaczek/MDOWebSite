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

        public IWebElement Title { get; set; }
        public IWebElement Content { get; set; }
        public string Type { get; set; }

        public ToastrMessages(WebDriverWait wait)
        {
            this.wait = wait;
        }
    }
}
