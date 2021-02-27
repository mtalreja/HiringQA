using Hiring.Autmation.Task.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Test.Utility.PageObjects;
using Test.Utility.Utilities;

namespace Hiring.Autmation.Task.StepsDefination
{
    [Binding]
    public class ClientPortalSteps
    {
        private ClientPortalPage _clientPortalPage;
        private UBSHomePage _ubsHomePage;

        public ClientPortalSteps(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _clientPortalPage = new ClientPortalPage(driver);
            _ubsHomePage = new UBSHomePage(driver);
        }

        [When(@"the client enters wrong email address '(.*)' and password '(.*)'")]
        public void WhenTheClientEntersWrongEmailAddress(string emailAddress, string password)
        {
            _ubsHomePage.NavigateToUBSLoginPage("Investment Bank Client Portal");
            _clientPortalPage.EnterClientDetails(emailAddress, password);
        }

        [Then(@"error message is displayed")]
        public void ThenErrorMessageIsDisplayed()
        {
            Assert.IsTrue(_clientPortalPage.IsErrorSectionDisplayed());
        }
    }
}
