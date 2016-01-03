using System;
using System.Collections.ObjectModel;
using System.Threading;
using Mdo.Acceptance.Selenium.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using PersistenceMocks;

namespace Mdo.Acceptance.Selenium
{
    public class MainPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private string url = @"http://localhost:12345/";
        private ToastrMessages toasts;

        public IWebElement LoginInput
        {
            get
            {
                return wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[1]/input")));
            }
        }

        public IWebElement PasswordInput
        {
            get
            {
                return wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[3]/input")));
            }
        }

        public IWebElement LoginButton
        {
            get { return wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[4]/a"))); }
        }

        public IWebElement LoginUsername
        {
            get { return wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/ul[2]/li"))); }
        }

        public ReadOnlyCollection<IWebElement> ToastrMessages
        {
            get
            {
                return wait.Until(d => d.FindElements(By.XPath("//*[@id='toast-container']/div")));
            }
        }

        public ToastrMessages Toasts => toasts;

        public MainPage(IWebDriver driver)
        {
            SetDriver(driver);
            GoToMainPage();
            InitializeElements();
        }

        public void Login(string username, string password)
        {
            LoginInput.SendKeys(username);
            PasswordInput.SendKeys(password);
            LoginButton.Click();

            WaitSomeTime();
        }

        public void Reset()
        {
            driver.Navigate().Refresh();
            driver.Manage().Cookies.DeleteAllCookies();
        }

        private void SetDriver(IWebDriver driver)
        {
            if (driver == null)
            {
                this.driver = new PhantomJSDriver();
            }
            this.driver = driver;
        }

        private void InitializeElements()
        {
            toasts = new ToastrMessages(wait);
            WaitSomeTime();
        }

        private void GoToMainPage()
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver: this.driver, timeout: new TimeSpan(0, 0, 0, 100));
        }

        private void WaitSomeTime(int timeInMs = 100)
        {
            Thread.Sleep(new TimeSpan(0, 0, 0, 0, timeInMs));
        }

        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
