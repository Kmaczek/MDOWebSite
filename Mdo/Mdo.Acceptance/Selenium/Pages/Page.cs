using System;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;

namespace Mdo.Acceptance.Selenium.Pages
{
    public abstract class Page
    {
        protected IWebDriver Driver { get; private set; }
        protected WebDriverWait Wait { get; private set; }
        public string Url { get; }

        protected Page(IWebDriver driver, string url)
        {
            this.Url = url;
            SetDriver(driver);
            CreateWaiter();
            InitializeElements();
        }

        private void SetDriver(IWebDriver driver)
        {
            if (driver == null)
            {
                this.Driver = new PhantomJSDriver();
                this.Driver.Navigate().GoToUrl(this.Url);
                this.Driver.Manage().Window.Maximize();
            }
            this.Driver = driver;
        }

        private void CreateWaiter()
        {
            this.Wait = new WebDriverWait(driver: this.Driver, timeout: new TimeSpan(0, 0, 0, 100));
        }

        private void InitializeElements()
        {
            TestHelpers.WaitSomeTime();
        }

        public void Cleanup()
        {
            this.Driver.Quit();
            this.Driver.Dispose();
        }

        public void Reset()
        {
            this.Driver.Navigate().Refresh();
            this.Driver.Manage().Cookies.DeleteAllCookies();
        }
    }
}
