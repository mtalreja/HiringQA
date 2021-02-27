using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Utility.Utilities;

namespace Test.Utility.PageObjects
{
    public class BasePage
    {
        protected IWebDriver Driver;

        /// <summary>
        /// Value of an element on the page which when visible indicates a particular page
        /// has been loaded fully
        /// </summary>
        protected By _pageLoadCompleteIndicatorLocator = null;

        protected string _title = "";

        public BasePage(IWebDriver driver, string pageTitle, By pageLoadCompleteIndicatorLocator)
        {
            Driver = driver;
            _title = pageTitle;
            _pageLoadCompleteIndicatorLocator = pageLoadCompleteIndicatorLocator;
        }

        /// <summary>
        /// Child classes that do not have page title and
        /// <see cref="_pageLoadCompleteIndicatorLocator"/> must override this method.
        /// Otherwise, this method will throw exception.
        /// </summary>
        /// <returns></returns>
        protected virtual bool WaitForPageToLoad()
        {
            return WebDriverExtensions.WaitForPageToLoad(Driver, _pageLoadCompleteIndicatorLocator, _title);
        }

        protected virtual bool ShortWaitForPageToLoad()
        {
            return WebDriverExtensions.WaitForPageToLoad(Driver, _pageLoadCompleteIndicatorLocator, _title, 1);
        }

        public virtual bool OnPage()
        {
            if (WaitForPageToLoad())
            {
                if (_pageLoadCompleteIndicatorLocator != null)
                {
                    return WebDriverExtensions.IsDisplayed(Driver.FindElement(_pageLoadCompleteIndicatorLocator));
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
