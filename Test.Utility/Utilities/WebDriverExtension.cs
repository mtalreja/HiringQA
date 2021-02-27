using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Utility.Utilities
{
    public class WebDriverExtensions
    {
        public static bool IsDisplayed(IWebElement element)
        {
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    return element.Displayed;
                }
                catch (InvalidCastException)
                {
                    System.Console.WriteLine("Saucelabs' .Displayed cast 'string' as 'bool' exception " + (i + 1) + " time...");
                }
            }
            return element.Displayed;
        }

        public static IWebElement WaitUntilVisible(IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutInSeconds));
            var element = wait.Until<IWebElement>(drv =>
            {
                try
                {
                    var elementToBeDisplayed = drv.FindElement(by);
                    if (IsDisplayed(elementToBeDisplayed))
                    {
                        return elementToBeDisplayed;
                    }
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
            return element;
        }

        public static IWebElement WaitUntilClickable(IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutInSeconds));
            var element = wait.Until<IWebElement>(drv =>
            {
                try
                {
                    var elementToBeClickable = drv.FindElement(by);
                    if (elementToBeClickable.Enabled)
                    {
                        return elementToBeClickable;
                    }
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
            return element;
        }

        public static void FillTextbox(IWebDriver driver, IWebElement element, string keys)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Click().SendKeys(keys).Perform();
        }

        public static string GetSelectedOptionText(IWebDriver driver, By locator)
        {
            IWebElement comboBox = driver.FindElement(locator);
            SelectElement selectedValue = new SelectElement(comboBox);
            return selectedValue.SelectedOption.Text;
        }

        public static void SelectByTextAndWait(IWebDriver driver, By locator, string text)
        {
            IWebElement element = WaitUntilVisible(driver, locator);
            SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByText(text);
            for (int i = 0; i < 200; i++)
            {
                if (selectElement.SelectedOption.Text == text)
                {
                    return;
                }
                System.Threading.Thread.Sleep(10);
            }
        }

        public static void ClearTextbox(IWebDriver driver, By by)
        {
            WaitUntilClickable(driver, by).Clear();
        }

        public static bool WaitForPageToLoad(IWebDriver driver, By by, string title = "", int waitFor = 10)
        {
            int count = 0;
            while (count < waitFor)
            {
                try
                {
                    if (IsDisplayed(WaitUntilVisible(driver, by)))
                    {
                        Console.WriteLine("Page loaded: " + title);
                        return true;
                    }
                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine("Page not loaded yet: " + title);
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("Page not loaded yet: " + title);
                }
                System.Threading.Thread.Sleep(10);
                count++;
            }
            return false;
        }

        public static void CloseAllWindows(IWebDriver driver)
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMilliseconds(1);
            foreach (string window in driver.WindowHandles)
            {
                try
                {
                    driver.SwitchTo().Window(window);
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("This window is still loading but we can still try to close it.");
                }
                catch (NoSuchWindowException)
                {
                    Console.WriteLine("This window disappeared, even better, one less window to close.");
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine("Other Exception with this window... try to close???");
                }

                try
                {
                    driver.Close();
                }
                catch (Exception)
                {
                    Console.WriteLine("I guess we can't close this window...");
                }
            }
        }

        public static void ClickWithActionBuilder(IWebDriver driver, IWebElement element)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Click().Perform();
        }

        public static void JsClickOn(IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }
    }
}
