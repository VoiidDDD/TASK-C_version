using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Tests
{
    public static class Driver
    {
        public static IWebDriver CreateDriver(string browser, Logger logger)
        {
            if (browser.Equals("edge"))
            {
                logger.Instance.Information("Edge driver created");
                return new EdgeDriver();
            }
            else if (browser.Equals("firefox"))
            {
                logger.Instance.Information("Firefox driver created");
                return new FirefoxDriver();
            }
            else
            {
                logger.Instance.Error("Creation of driver failed!");
                throw new Exception();
            }
        }
    }
}
