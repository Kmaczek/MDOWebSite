using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;

namespace Mdo.Acceptance
{
    [TestClass]
    public class UserLoginTest
    {
        IWebDriver phantomdDriver;
        private WebDriverWait wait;
        private string url = @"http://localhost:12345/";

        [TestInitialize]
        public void Setup()
        {
            phantomdDriver = new PhantomJSDriver();
            phantomdDriver.Navigate().GoToUrl(url);
            phantomdDriver.Manage().Window.Maximize();
            wait = new WebDriverWait(phantomdDriver, new TimeSpan(0, 0, 0, 100));
        }

        [TestCleanup]
        public void Cleanup()
        {
            phantomdDriver.Quit();
            phantomdDriver.Dispose();
        }

        private void WaitSomeTime()
        {
            Thread.Sleep(new TimeSpan(0, 0, 0, 0, 100));
        }

        [TestMethod]
        public void afeter_successfull_logging_in_username_should_be_displayed()
        {
            var login = wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[1]/input")));
            var pass = wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[3]/input")));
            var loginBtn = wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[4]/a")));

            login.SendKeys("dk");
            pass.SendKeys("dk");
            loginBtn.Click();

            WaitSomeTime();

            var result = wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/ul[2]/li")));
            Assert.IsTrue(result.Text.Contains("dk"), result.Text + ": Should contain 'dk'");
        }

        [TestMethod]
        public void afeter_successffull_logging_in_message_message_is_displayed()
        {
            var login = wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[1]/input")));
            var pass = wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[3]/input")));
            var loginBtn = wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[4]/a")));

            login.SendKeys("dk");
            pass.SendKeys("dk");
            loginBtn.Click();
            var message = wait.Until(d => d.FindElement(By.XPath("//*[@id='toast-container']/div/div[1]/div")));
            Assert.IsTrue(message.Text.ToLowerInvariant().Contains("login successful"));
        }
    }
}
