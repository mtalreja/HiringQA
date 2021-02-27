using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Utility.PageObjects;
using Test.Utility.Utilities;

namespace Hiring.Autmation.Task.PageObjects
{
    public class JobSearchPage : BasePage
    {
        public const string _signInLocatorXpath = "//span[text()='Sign In']";
        private By _signInLocator = By.XPath(_signInLocatorXpath);
        private By _dontHaveAnAccountLocator = By.XPath("//*[@id='signInForm_BUTTON_4']/following-sibling::div/a");

        public JobSearchPage(IWebDriver driver) : base(driver, "Job Search", By.XPath(_signInLocatorXpath))
        {

        }

        public void ClickSignIn()
        {
            WaitForPageToLoad();
            Driver.FindElement(_signInLocator).Click();
        }

        public void ClickDontHaveAnAccountLocator()
        {
            var elem = WebDriverExtensions.WaitUntilVisible(Driver, _dontHaveAnAccountLocator);
            WebDriverExtensions.JsClickOn(Driver, elem);
        }
    }
}
