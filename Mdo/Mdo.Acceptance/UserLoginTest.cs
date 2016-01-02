using System;
using System.Linq;
using System.Threading;
using Mdo.Acceptance.Selenium;
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
        private static IWebDriver driver = new PhantomJSDriver();
        private MainPage mainPage;

        [ClassCleanup]
        public static void ClassCleanup()
        {
            driver.Quit();
            driver.Dispose();
        }

        [TestInitialize]
        public void Setup()
        {
            if (mainPage == null)
            {
                mainPage = new MainPage(driver);
            }

            WaitSomeTime();
            mainPage.Reset();
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        private void WaitSomeTime(int timeInMs = 100)
        {
            Thread.Sleep(new TimeSpan(0, 0, 0, 0, timeInMs));
        }

        [TestMethod]
        public void afeter_successfull_logging_in_username_should_be_displayed()
        {
            mainPage.Login("dk", "dk");

            Assert.IsTrue(mainPage.LoginUsername.Text.Contains("dk"));
        }

        [TestMethod]
        public void afeter_successffull_logging_in_message_is_displayed()
        {
            mainPage.Login("dk", "dk");

            var message = mainPage.ToastrMessages.First();
            Assert.IsTrue(message.Text.ToLowerInvariant().Contains("login successful"));
        }
    }
}
