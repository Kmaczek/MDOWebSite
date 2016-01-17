using Mdo.Acceptance.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using PersistenceMocks;

namespace Mdo.Acceptance.Tests
{
    [TestClass]
    public class UserSessionTest
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
        public void logging_in_then_refreshing_page_should_lave_user_still_loged_on()
        {
            mainPage.Login(UserWarehouse.StdUsername, UserWarehouse.StdPassword);
            Assert.IsTrue(mainPage.IsUserLogedIn(UserWarehouse.StdUsername));

            mainPage.Refresh();
            Assert.IsTrue(mainPage.IsUserLogedIn(UserWarehouse.StdUsername));
        }

        [TestMethod]
        public void logging_out_then_refreshing_page_should_not_make_user_loged_on()
        {
            mainPage.Login(UserWarehouse.StdUsername, UserWarehouse.StdPassword);
            Assert.IsTrue(mainPage.IsUserLogedIn(UserWarehouse.StdUsername));

            mainPage.Logout();
            Assert.IsTrue(!mainPage.IsUserLogedIn(UserWarehouse.StdUsername));

            mainPage.Refresh();
            Assert.IsTrue(!mainPage.IsUserLogedIn(UserWarehouse.StdUsername));
        }
    }
}
