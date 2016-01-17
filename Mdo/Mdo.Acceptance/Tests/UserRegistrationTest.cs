using Mdo.Acceptance.Selenium;
using Mdo.Acceptance.Selenium.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using PersistenceMocks;

namespace Mdo.Acceptance.Tests
{
    [TestClass]
    public class UserRegistrationTest
    {
        private static IWebDriver driver = new PhantomJSDriver();
        private MainPage mainPage;
        private RegisterUserPage registerPage;

        public UserRegistrationTest()
        {
        }

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

            if (registerPage == null)
            {
                registerPage = new RegisterUserPage(driver);
            }

            TestHelpers.WaitSomeTime();
            mainPage.Reset();
        }

        [TestMethod]
        public void clicking_on_register_should_navigate_to_registration_page()
        {
            mainPage.RegisterButton.Click();

            Assert.IsTrue(driver.Url.Contains(@"/user/register"));
        }

        [TestMethod]
        public void specifying_correct_parameters_should_create_user()
        {
            var newUser = UserWarehouse.GenerateUser("john");
            registerPage.Register(newUser);
            TestHelpers.WaitSomeTime(1000);

            var storedUser = UserWarehouse.GetUser(x => x.Username == newUser.Username);
            Assert.IsNotNull(storedUser);
        }
    }
}
