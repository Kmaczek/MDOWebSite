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
        public WebDriverWait Wait { get; private set; }
        private string url = TestHelpers.PageUrl;
        private ToastrMessages toasts;

        public IWebElement LoginInput
        {
            get
            {
                return Wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[1]/input")));
            }
        }

        public IWebElement PasswordInput
        {
            get
            {
                return Wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[3]/input")));
            }
        }

        public IWebElement LoginButton
        {
            get { return Wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[4]/a"))); }
        }

        public IWebElement RegisterButton
        {
            get { return Wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[5]/a"))); }
        }

        public IWebElement LoginUsername
        {
            get { return Wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/ul[2]/li"))); }
        }

        public IWebElement LogoutButton
        {
            get { return Wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/ul[2]/li[2]/a"))); }
        }

        public ReadOnlyCollection<IWebElement> ToastrMessages
        {
            get
            {
                return Wait.Until(d => d.FindElements(By.XPath("//*[@id='toast-container']/div")));
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

            TestHelpers.WaitForHttpRequest();
        }

        public void Logout()
        {
            LogoutButton.Click();
            TestHelpers.WaitSomeTime(30);
        }

        public bool IsUserLogedIn(string username)
        {
            if (LoginUsername.Displayed
                && LoginUsername.Text.Contains(username)
                && LogoutButton.Displayed)
            {
                return true;
            }
            return false;
        }

        public void Reset()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().Refresh();
        }

        public void Refresh()
        {
            driver.Navigate().Refresh();
            TestHelpers.WaitSomeTime(10);
        }

        private void SetDriver(IWebDriver fdriver)
        {
            if (fdriver == null)
            {
                this.driver = new PhantomJSDriver();
            }
            this.driver = fdriver;
        }

        private void InitializeElements()
        {
            toasts = new ToastrMessages(driver);
            TestHelpers.WaitSomeTime();
        }

        private void GoToMainPage()
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            Wait = new WebDriverWait(driver: this.driver, timeout: new TimeSpan(0, 0, 0, 300));
        }

//        private void WaitSomeTime(int timeInMs = 100)
//        {
//            Thread.Sleep(new TimeSpan(0, 0, 0, 0, timeInMs));
//        }

        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
