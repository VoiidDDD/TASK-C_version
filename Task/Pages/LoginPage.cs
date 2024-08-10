using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Tests
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly Logger logger;
        public LoginPage(IWebDriver driver, Logger logger)
        {
            this.driver = driver;
            this.logger = logger;
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        public IWebElement UsernameField => driver.FindElement(By.CssSelector("#user-name"));
        public IWebElement PasswordField => driver.FindElement(By.CssSelector("#password"));
        public IWebElement LoginButton => driver.FindElement(By.CssSelector("#login-button"));
        public IWebElement ErrorMessage => driver.FindElement(By.CssSelector(".error-message-container"));
        public IWebElement Dashboard => driver.FindElement(By.CssSelector(".app_logo"));

        public void Login(string username, string password)
        {
            UsernameField.SendKeys(username);
            PasswordField.SendKeys(password);
            logger.Instance.Information("Login fields written");
        }
        public void Click()
        {
            LoginButton.Click();
            logger.Instance.Information("Login button clicked");
        }

        public void ClearInputs(bool user = true, bool password = true)
        {
            if (user)
            {
                UsernameField.SendKeys(Keys.Control + "a");
                UsernameField.SendKeys(Keys.Delete);
            }
            if (password)
            {
                PasswordField.SendKeys(Keys.Control + "a");
                PasswordField.SendKeys(Keys.Delete);
            }
            logger.Instance.Information("Login fields cleared");
        }
        public bool Validate(string str)
        {
            return Dashboard.Text.Equals(str);
        }
    }
}
