using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using Serilog;
using Task.Tests;
[assembly: Parallelize(Workers = 6, Scope = ExecutionScope.MethodLevel)]
namespace Task
{
    [TestClass]
    public class UnitTest1
    {
        private Logger logger;

        [TestInitialize]
        public void Setup()
        {
            logger = new Logger();
        }

        [TestMethod]
        [DataRow("edge", "standard_user", "secret_sauce")]
        //[DataRow("firefox", "visual_user", "secret_sauce")]
        public void UC_1(string browser,string user,string password)
        {
            //Logger
            logger.Instance.Information("START OF UC_1");
            //Driver Creation
            IWebDriver driver = Driver.CreateDriver(browser, logger);

            //Test
            LoginPage loginPage = new LoginPage(driver, logger);
            loginPage.Login(user, password);
            loginPage.ClearInputs();
            loginPage.Click();
            Assert.IsTrue(loginPage.ErrorMessage.Text.Contains("Username is required"));

            //Driver Disposition
            driver.Dispose();
            //Logger
            logger.Instance.Information("END OF UC_1");
        }

        [TestMethod]
        [DataRow("edge", "standard_user", "secret_sauce")]
        //[DataRow("firefox", "visual_user", "secret_sauce")]
        public void UC_2(string browser, string user, string password)
        {
            //Logger
            logger.Instance.Information("START OF UC_2");
            //Driver Creation
            IWebDriver driver = Driver.CreateDriver(browser, logger);

            //Test
            LoginPage loginPage = new LoginPage(driver, logger);
            loginPage.Login(user, password);
            loginPage.ClearInputs(false,true);
            loginPage.Click();
            Assert.IsTrue(loginPage.ErrorMessage.Text.Contains("Password is required"));

            //Driver Disposition
            driver.Dispose();
            //Logger
            logger.Instance.Information("END OF UC_2");
        }

        [TestMethod]
        [DataRow("edge", "standard_user", "secret_sauce")]
        //[DataRow("firefox", "visual_user", "secret_sauce")]
        public void UC_3(string browser, string user, string password)
        {
            //Logger
            logger.Instance.Information("START OF UC_3");
            //Driver Creation
            IWebDriver driver = Driver.CreateDriver(browser, logger);

            //Test
            LoginPage loginPage = new LoginPage(driver, logger);
            loginPage.Login(user, password);
            loginPage.Click();
            Assert.IsTrue(loginPage.Validate("Swag Labs"));

            //Driver Disposition
            driver.Dispose();
            //Logger
            logger.Instance.Information("END OF UC_3");
        }
    }
}