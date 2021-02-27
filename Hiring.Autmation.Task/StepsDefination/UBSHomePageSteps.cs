using Hiring.Autmation.Task.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace Hiring.Autmation.Task.StepsDefination
{
    [Binding]
    public class UBSHomePageSteps
    {
        private UBSHomePage _ubsHomePage;

        public UBSHomePageSteps(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _ubsHomePage = new UBSHomePage(driver);
        }

        [Given(@"the user starts browsing the UBS website")]
        public void GivenTheUserStartsBrowsingTheUBSWebsite()
        {
            _ubsHomePage.OpenUBSHomePage();
        }

        [When(@"the user changes '(.*)' with '(.*)'")]
        public void WhenTheUserChangesTheLocation(string region, string country)
        {
            _ubsHomePage.SetDomicile(region, country);
        }

        [Then(@"the user sees the domicile is updated to '(.*)'")]
        public void ThenTheUserSeesTheLocationIsUpdatedTo(string country)
        {
            Assert.IsTrue(_ubsHomePage.IsDomicileCountrySet(country));
        }
    }
}
