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
    public class UnitTest1
    {
        IWebDriver phantomdDriver;
        private string url = @"http://localhost:12345/";

        [TestInitialize]
        public void Setup()
        {
            phantomdDriver = new PhantomJSDriver();
        }

        [TestCleanup]
        public void Cleanup()
        {
            phantomdDriver.Quit();
            phantomdDriver.Dispose();
        }

        [TestMethod]
        public void PhantomTest()
        {
            phantomdDriver.Navigate().GoToUrl(url);
            phantomdDriver.Manage().Window.Maximize();
            Thread.Sleep(new TimeSpan(0, 0, 0, 3));

            WebDriverWait wait = new WebDriverWait(phantomdDriver, new TimeSpan(0, 0, 0, 10));

            var login = wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[1]/input")));
            var pass = wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[3]/input")));
            var loginBtn = wait.Until(d => d.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/form/ul/li[4]/a")));

            login.SendKeys("dk");
            pass.SendKeys("dk");
            loginBtn.Click();
            var result = phantomdDriver.FindElement(By.XPath("/html/body/mdo-nav/div/div/div/ul[2]/li"));
            Assert.IsTrue(result.Text.Contains("wes"));

         }
    }
}
