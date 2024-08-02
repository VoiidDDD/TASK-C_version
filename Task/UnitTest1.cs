using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using Serilog;
[assembly: Parallelize(Workers = 6, Scope = ExecutionScope.MethodLevel)]
namespace Task
{
    [TestClass]
    public class UnitTest1
    {
        public class LoginPage
        {
            private IWebDriver driver;
            private ILogger logger;
            public LoginPage(IWebDriver driver, ILogger logger)
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
                logger.Information("Login fields written");
            }
            public void Click()
            {
                LoginButton.Click();
                logger.Information("Login button clicked");
            }

            public void ClearInputs(bool user = true,bool password = true)
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
                logger.Information("Login fields cleared");
            }
            public bool Validate(string str)
            {
                return Dashboard.Text.Equals(str);
            }
        }
        public static IWebDriver CreateDriver(string browser,ILogger logger)
        {
            if(browser.Equals("edge"))
            {
                logger.Information("Edge driver created");
                return new EdgeDriver();
            }else if(browser.Equals("firefox"))
            {
                logger.Information("Firefox driver created");
                return new FirefoxDriver();
            }
            else
            {
                logger.Error("Creation of driver failed!");
                throw new Exception();
            }
        }
        private ILogger logger;
        [TestInitialize]
        public void Setup()
        {
            logger = new LoggerConfiguration().WriteTo.File("logs.txt").WriteTo.Console().CreateLogger();
        }
        [TestMethod]
        [DataRow("edge", "standard_user", "secret_sauce")]
        [DataRow("firefox", "visual_user", "secret_sauce")]
        public void UC_1(string browser,string user,string password)
        {
            logger.Information("START OF UC_1");
            IWebDriver driver = CreateDriver(browser,logger);
            LoginPage loginPage = new LoginPage(driver, logger);
            loginPage.Login(user, password);
            loginPage.ClearInputs();
            loginPage.Click();
            Assert.IsTrue(loginPage.ErrorMessage.Text.Contains("Username is required"));
            driver.Dispose();
            logger.Information("END OF UC_1");
        }
        [TestMethod]
        [DataRow("edge", "standard_user", "secret_sauce")]
        [DataRow("firefox", "visual_user", "secret_sauce")]
        public void UC_2(string browser, string user, string password)
        {
            logger.Information("START OF UC_2");
            IWebDriver driver = CreateDriver(browser, logger);
            LoginPage loginPage = new LoginPage(driver, logger);
            loginPage.Login(user, password);
            loginPage.ClearInputs(false,true);
            loginPage.Click();
            Assert.IsTrue(loginPage.ErrorMessage.Text.Contains("Password is required"));
            driver.Dispose();
            logger.Information("END OF UC_2");
        }
        [TestMethod]
        [DataRow("edge", "standard_user", "secret_sauce")]
        [DataRow("firefox", "visual_user", "secret_sauce")]
        public void UC_3(string browser, string user, string password)
        {
            logger.Information("START OF UC_3");
            IWebDriver driver = CreateDriver(browser, logger);
            LoginPage loginPage = new LoginPage(driver, logger);
            loginPage.Login(user, password);
            loginPage.Click();
            Assert.IsTrue(loginPage.Validate("Swag Labs"));
            driver.Dispose();
            logger.Information("END OF UC_3");
        }
    }
}