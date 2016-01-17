using Mdo.Acceptance.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using PersistenceMocks;

namespace Mdo.Acceptance.Tests
{
    [TestClass]
    public class UserLogoutTest
    {
        private static readonly IWebDriver driver = new PhantomJSDriver();
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

            TestHelpers.WaitSomeTime();
            mainPage.Reset();
        }

        [TestMethod]
        public void loged_in_users_should_be_able_to_log_out()
        {
            mainPage.Login(UserWarehouse.StdUsername, UserWarehouse.StdPassword);
            Assert.IsTrue(mainPage.IsUserLogedIn(UserWarehouse.StdUsername));

            mainPage.Logout();
            Assert.IsTrue(!mainPage.IsUserLogedIn(UserWarehouse.StdUsername));
        }
    }
}
