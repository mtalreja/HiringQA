using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System;

namespace Test.Utility.Factories
{
    public class DriverFactory
    {
        public static IWebDriver GetWebDriver(string browserName)
        {
            string browser = string.IsNullOrWhiteSpace(browserName) ? BrowserConstant.CHROME : browserName;

            var webDriver = GetLocalWebDriver(browser);
            webDriver.Manage().Window.Maximize();
            return webDriver;
        }

        private static IWebDriver GetLocalWebDriver(string browser)
        {
            switch (browser.ToLowerInvariant())
            {
                case BrowserConstant.CHROME:
                    ChromeOptions options = new ChromeOptions();
                    return new ChromeDriver(options);
                case BrowserConstant.EDGE:
                    return new EdgeDriver();
                case BrowserConstant.FIREFOX:
                    return new FirefoxDriver();
                case BrowserConstant.IE:
                    return new InternetExplorerDriver();
                case BrowserConstant.SAFARI:
                    return new SafariDriver();
                default:
                    throw new ArgumentException($"Browser not yet implemented: {browser}");
            }
        }
    }
}
