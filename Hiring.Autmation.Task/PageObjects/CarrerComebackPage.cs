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
    public class CareerComebackPage : BasePage
    {
        private const string _applyNowXpath = "//*[@id='slick-slide00']//*[text()='Apply Now']";
        private By _applyNowSelector = By.XPath(_applyNowXpath);

        public CareerComebackPage(IWebDriver driver) : base(driver, "Career Comeback | UBS Global", By.XPath(_applyNowXpath))
        {

        }

        public void ClickApplyNowButton()
        {
            WaitForPageToLoad();
            var applyNowBtn = Driver.FindElement(_applyNowSelector);
            WebDriverExtensions.ClickWithActionBuilder(Driver, applyNowBtn);
        }
    }
}
