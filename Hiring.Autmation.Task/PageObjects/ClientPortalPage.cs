using OpenQA.Selenium;
using Test.Utility.PageObjects;
using Test.Utility.Utilities;

namespace Hiring.Autmation.Task.PageObjects
{
    public class ClientPortalPage : BasePage
    {
        private By _emailAddressLocator = By.XPath("//input[@class = 'verify-email-input input-field']");
        private By _passwordLocator = By.XPath("//input[@class = 'password-input input-field']");
        private By _emailNextBtnLocator = By.XPath("//button[@class = 'verify-email-button btn btn-primary']");
        private By _passwordNextBtnLocator = By.XPath("//button[@class = 'submit-1fa-password-button btn btn-primary']");
        private By _errorSection = By.CssSelector("body > div.outer-section.neo_background > div.content.hide > div.content-section > div.login-options-section.hide.view > div:nth-child(5) > p.error-section.hide");
        public ClientPortalPage(IWebDriver driver) : base(driver, "UBS Client Portal", By.XPath("//h1[text()='Log in']"))
        {

        }

        public void EnterClientDetails(string emailAddress, string password)
        {
            WaitForPageToLoad();
            var emailAddressTextBox = WebDriverExtensions.WaitUntilClickable(Driver, _emailAddressLocator);
            WebDriverExtensions.FillTextbox(Driver, emailAddressTextBox, emailAddress);
            WebDriverExtensions.WaitUntilClickable(Driver, _emailNextBtnLocator).Click();
            var pwdTextBox = WebDriverExtensions.WaitUntilVisible(Driver, _passwordLocator);
            pwdTextBox.Click();
            WebDriverExtensions.FillTextbox(Driver, pwdTextBox, password);
            WebDriverExtensions.WaitUntilClickable(Driver, _passwordNextBtnLocator).Click();
        }

        public bool IsErrorSectionDisplayed()
        {
            return WebDriverExtensions.WaitUntilVisible(Driver, _errorSection, 20).Displayed;
        }
    }
}
