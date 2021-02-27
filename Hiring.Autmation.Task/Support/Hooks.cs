using BoDi;
using OpenQA.Selenium;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using Test.Utility.Factories;
using Test.Utility.Utilities;

namespace Hiring.Autmation.Task.Support
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private IWebDriver _driver;

        public Hooks(ScenarioContext scenarioContext)
        {
            this._objectContainer = scenarioContext.ScenarioContainer;
        }

        [BeforeScenario(Order = 0)]
        public void ScenarioStart()
        {
            ScenarioInfo scenario = _objectContainer.Resolve<ScenarioInfo>();
            Console.WriteLine("Starting scenario: " + scenario.Title);
        }

        [BeforeScenario("browser", Order = 1)]
        public void BrowserCreate()
        {
            var browserName = ConfigurationManager.AppSettings.Get("Browser");
            _driver = DriverFactory.GetWebDriver(browserName);
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
        }

        [AfterScenario("browser")]
        public void BrowserDestroy()
        {
            if (_driver != null)
            {
                WebDriverExtensions.CloseAllWindows(_driver);
            }
            if (_driver != null)
            {
                _driver.Quit();
            }
        }

    }
}
