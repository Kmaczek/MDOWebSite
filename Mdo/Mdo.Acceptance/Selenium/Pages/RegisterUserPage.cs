using Mdo.DB.Entities;
using OpenQA.Selenium;

namespace Mdo.Acceptance.Selenium.Pages
{
    public class RegisterUserPage : Page
    {
        public IWebElement UsernameInput
        {
            get
            {
                return this.Wait.Until(d => d.FindElement(By.XPath(@"//*[@id='input-username']")));
            }
        }

        public IWebElement EmailInput
        {
            get
            {
                return this.Wait.Until(d => d.FindElement(By.XPath(@"//*[@id='input-email']")));
            }
        }

        public IWebElement PasswordInput
        {
            get
            {
                return this.Wait.Until(d => d.FindElement(By.XPath(@"//*[@id='input-password']")));
            }
        }

        public IWebElement RepeatInput
        {
            get
            {
                return this.Wait.Until(d => d.FindElement(By.XPath(@"//*[@id='input-password-repeat']")));
            }
        }

        public IWebElement RegisterButton
        {
            get
            {
                return this.Wait.Until(d => d.FindElement(By.XPath(@"/html/body/div[1]/div/div[2]/ui-view/div/form/div/button")));
            }
        }

        public RegisterUserPage(IWebDriver driver) : base(driver, TestHelpers.UserRegisterUrl) //PageUrl+"/user/register"
        {
        }

        public void Register(string username, string email, string password, string repeat)
        {
            // debounce is the time after which asynchronous validation in UI will kick in
            int debounce = 1020;
            this.UsernameInput.SendKeys(username);
            TestHelpers.WaitSomeTime(debounce);
            this.EmailInput.SendKeys(email);
            TestHelpers.WaitSomeTime(debounce);
            this.PasswordInput.SendKeys(password);
            TestHelpers.WaitSomeTime(debounce);
            this.RepeatInput.SendKeys(repeat);
            TestHelpers.WaitSomeTime(debounce);
            
            this.RegisterButton.Click();
        }

        public void Register(UserEntity user)
        {
            this.Register(user.Username, user.Email, user.Password, user.Password);
        }
    }
}
