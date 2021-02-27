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
    public class PrivacyPolicyPage : BasePage
    {
        private const string _aggreeBtnXpath = "//span[text()='Agree']//parent::button";
        private IWebElement _aggreeBtnElem => Driver.FindElement(By.XPath(_aggreeBtnXpath));
        public PrivacyPolicyPage(IWebDriver driver) : base(driver, "Privacy Policy", By.XPath(_aggreeBtnXpath))
        {

        }

        public void AggreePrivacyPolicy()
        {
            WaitForPageToLoad();
            WebDriverExtensions.JsClickOn(Driver, _aggreeBtnElem);
        }

    }
}
