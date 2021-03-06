﻿using System;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading;
using Mdo.Acceptance.Selenium;
using Mdo.Acceptance.Selenium.Elements;
using Mdo.Acceptance.Selenium.Extensions;
using Mdo.Acceptance.Selenium.Pages;
using Mdo.DB.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using PersistenceMocks;
using SharedData;

namespace Mdo.Acceptance.Tests
{
    [TestClass]
    public class UserRegistrationTest
    {
        private static IWebDriver driver = new PhantomJSDriver();
        private MainPage mainPage;
        private RegisterUserPage registerPage;
        private ToastrMessages toasts;
        private static UserWarehouse userWarehouse;

        public UserRegistrationTest()
        {
        }

        [ClassInitialize]
        public static void Setup(TestContext cont)
        {
            userWarehouse = SetupAssemblyInitializer.UserWarehouse;
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

            if (toasts == null)
            {
                this.toasts = new ToastrMessages(driver);
            }
            
            TestHelpers.WaitSomeTime();
            mainPage.Reset();
        }

        [TestCleanup]
        public void Cleanup()
        {
            userWarehouse.ResetUsers();
        }

        [TestMethod]
        public void clicking_on_register_should_navigate_to_registration_page()
        {
            mainPage.RegisterButton.Click();

            Assert.IsTrue(driver.Url.Contains(@"/user/register"));
        }

        [TestMethod]
        public void specifying_correct_parameters_should_create_user_and_display_message()
        {
            var newUser = userWarehouse.GenerateUser("john");
            registerPage.Register(newUser);
            TestHelpers.WaitForHttpRequest();
  
            var storedUser = Waiter.ForNotNull(() => userWarehouse.GetUserByName(newUser.Username));
            Assert.IsNotNull(storedUser);

            Assert.IsTrue(driver.Url.Contains("message"));
        }
    }
}
