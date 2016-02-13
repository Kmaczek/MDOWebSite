using Mdo.Acceptance.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using PersistenceMocks;

namespace Mdo.Acceptance.Tests
{
    [TestClass]
    public class PermissionsTest
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

            TestHelpers.WaitSomeTime(50);
            mainPage.Reset();
        }

        [TestMethod]
        public void authorized_cannot_access_registration_and_is_redirected_instead()
        {
            mainPage.ValidLogin();
            driver.Navigate().GoToUrl(TestHelpers.UserRegisterUrl);
            TestHelpers.WaitSomeTime();

            Assert.AreEqual(TestHelpers.UserProfileUrl, driver.Url);
            Assert.AreNotEqual(TestHelpers.UserRegisterUrl, driver.Url);
        }

        [TestMethod]
        public void unauthorized_cannot_access_user_profile_and_is_redirected_instead()
        {
            driver.Navigate().GoToUrl(TestHelpers.UserProfileUrl);
            Assert.AreEqual("Unauthorized", mainPage.MessagesPage.Title.Text);
            Assert.AreEqual("Please log in to access page", mainPage.MessagesPage.Content.Text);
        }
    }
}
