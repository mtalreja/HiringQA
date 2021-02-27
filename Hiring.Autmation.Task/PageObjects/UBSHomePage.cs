using System.Configuration;
using System.Linq;
using OpenQA.Selenium;
using Test.Utility.PageObjects;
using Test.Utility.Utilities;

namespace Hiring.Autmation.Task.PageObjects
{
    public class UBSHomePage : BasePage
    {
        private By _regionSelector = By.CssSelector("#metanavigation-navContent0 > li > ul > li.domicileSelection__itemWrapper.domicileSelection__item--region > button");
        private By _regionSectionSelector => By.Id("domicileSelection__items-navContent0");
        private By _domicileCountrySelector => By.CssSelector("#metanavigation-navContent0 > li > ul > li.domicileSelection__itemWrapper.domicileSelection__item--country > button");
        private By _domicileSelector => By.Id("domicileButton");
        private IWebElement _domicileBtn => Driver.FindElement(_domicileSelector);
        private IWebElement _headerLoginBtn => Driver.FindElement(By.Id("headerLoginToggleButton"));
        private By _domicileCountrySectionSelector => By.Id("domicileSelection__items-navContent1");
        private By _privacyOkSelector => By.XPath("//*[@id='doc']//*[text()='OK']");

        public UBSHomePage(IWebDriver driver) : base(driver, "Our financial services in your location", By.Id("headerLoginToggleButton"))
        {

        }

        public void OpenUBSHomePage()
        {
            string url = ConfigurationManager.AppSettings.Get("URL");
            if (!string.IsNullOrWhiteSpace(url))
            {
                Driver.Navigate().GoToUrl(url);
                WaitForPageToLoad();
            }
        }

        public void SetDomicile(string region, string country)
        {
            if (!IsDomicileCountrySet(country))
            {
                WebDriverExtensions.WaitUntilClickable(Driver, _domicileSelector).Click();

                WebDriverExtensions.WaitUntilVisible(Driver, _regionSelector).Click();
                var regionSection = WebDriverExtensions.WaitUntilVisible(Driver, _regionSectionSelector);

                var regions = regionSection.FindElements(By.XPath("//li"));
                if (regions != null)
                {
                    regions.FirstOrDefault(x => x.Text == region).Click();
                }

                WebDriverExtensions.WaitUntilClickable(Driver, _domicileCountrySelector).Click();
                var countrySection = WebDriverExtensions.WaitUntilVisible(Driver, _domicileCountrySectionSelector);

                var domicileCountrySection = countrySection.FindElements(By.XPath("//li"));
                if (domicileCountrySection != null)
                {
                    domicileCountrySection.FirstOrDefault(x => x.Text == country).Click();
                }
            }
        }

        public bool IsDomicileCountrySet(string country)
        {
            return _domicileBtn.Text == country;
        }

        public void NavigateToUBSLoginPage(string loginTo)
        {
            _headerLoginBtn.Click();
            By _ubsLoginBtn = By.XPath($"//*[@id='metanavigation-navContent3']//a[text()='{loginTo}']");
            var navigateToLoginBtn = WebDriverExtensions.WaitUntilVisible(Driver, _ubsLoginBtn, 20);
            if (navigateToLoginBtn != null)
            {
                navigateToLoginBtn.Click();
            }
        }

        public void NavigateToLink(string parentName, string childText)
        {
            var parentElem = Driver.FindElements(By.XPath("//*[@id='mainmenu']/li/button")).FirstOrDefault(x => x.Text == parentName);
            parentElem.Click();
            var index = parentName == "Investment Bank" ? "0" : parentName == "About Us" ? "1" : "2";
            var childSectionDiv = WebDriverExtensions.WaitUntilVisible(Driver, By.XPath($"//*[@id='mainmenu-navContent{index}']"));
            WebDriverExtensions.WaitUntilVisible(Driver, By.XPath($"//span/a[text()='{childText}']")).Click();
        }

        public void ClickPrivacyOkBtn()
        {
            var okPrivacyBtn = Driver.FindElement(_privacyOkSelector);
            if (okPrivacyBtn != null)
                okPrivacyBtn.Click();
        }
    }
}
