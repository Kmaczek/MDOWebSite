using System;
using System.Linq;
using System.Threading;
using Mdo.Acceptance.Selenium;
using Mdo.Acceptance.Selenium.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using PersistenceMocks;

namespace Mdo.Acceptance.Tests
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
            mainPage.Login(UserWarehouse.StdUsername, UserWarehouse.StdPassword);

            Assert.IsTrue(mainPage.LoginUsername.Text.Contains(UserWarehouse.StdUsername));
        }

        [TestMethod]
        public void logging_with_email_is_possible()
        {
            mainPage.Login(UserWarehouse.StdEmail, UserWarehouse.StdPassword);

            Assert.IsTrue(mainPage.LoginUsername.Text.Contains(UserWarehouse.StdUsername));
        }

        [TestMethod]
        public void afeter_successffull_logging_in_message_is_displayed()
        {
            mainPage.Login(UserWarehouse.StdUsername, UserWarehouse.StdPassword);

            var toast = mainPage.Toasts.Messages.First();
            Assert.IsTrue(toast.GetToastMessage().Text.ToLowerInvariant().Contains("login successful"));
        }

        [TestMethod]
        public void giving_incorrect_credentials_will_fail_logging_in_and_display_message()
        {
            mainPage.Login(UserWarehouse.FakeUsername, UserWarehouse.StdPassword);

            var toast = mainPage.Toasts.Messages.First();
            Assert.IsTrue(toast.GetToastMessage().Text.ToLowerInvariant().Contains("incorrect login, e-mail or password"));
            Assert.IsTrue(toast.GetToastType().ToLowerInvariant().Contains("error"));
            toast.Click();

            mainPage.Login(UserWarehouse.StdUsername, UserWarehouse.FakePassword);
            WaitSomeTime();
            var wrongPassToast = mainPage.Toasts.Messages.First();
            Assert.IsTrue(wrongPassToast.GetToastMessage().Text.ToLowerInvariant().Contains("incorrect login, e-mail or password"));
        }
    }
}
