using Hiring.Autmation.Task.Modals;
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
    public class CreateNewAccountPage : BasePage
    {
        private IWebElement _emailAddressElem => Driver.FindElement(By.Id("username"));
        private IWebElement _pwdElem => Driver.FindElement(By.Id("password"));
        private IWebElement _reEnterPwdElem => Driver.FindElement(By.Id("confirmPassword"));
        private IWebElement _attentionErrorElem => Driver.FindElement(By.XPath("//*[@id='createAccountForm']//span[contains(text(),'attention')]"));

        private IWebElement _continueBtnElem => Driver.FindElement(By.XPath("//span[text()='Continue']"));

        public CreateNewAccountPage(IWebDriver driver) : base(driver, "Create new account", By.Id("createOwnTab"))
        {

        }

        public void EnterNewUserDetails(User user)
        {
            int i = 1;
            WaitForPageToLoad();
            WebDriverExtensions.FillTextbox(Driver, _emailAddressElem, user.EmailAddress);
            WebDriverExtensions.FillTextbox(Driver, _pwdElem, user.Password);
            WebDriverExtensions.FillTextbox(Driver, _reEnterPwdElem, user.Password);

            if (user.QuestionAnswers.Count > 3)
                throw new Exception("Input for the Questions/Answers did not match the UI");

            foreach (var questAndDict in user.QuestionAnswers)
            {
                WebDriverExtensions.WaitUntilClickable(Driver, By.Id($"selectSecurityQuestion{i}-button_text")).Click();
                WebDriverExtensions.WaitUntilVisible(Driver, By.XPath($"//*[@id='selectSecurityQuestion{i}-menu']//li//*[text()='{questAndDict.Item1}']")).Click();
                var answerElem = Driver.FindElement(By.Id($"securityQuestion{i}Answer"));
                WebDriverExtensions.FillTextbox(Driver, answerElem, questAndDict.Item2);
                i++;
            }
        }

        public void ClickContinueBtn()
        {
            _continueBtnElem.Click();
        }

        public bool IsAttentionErrorDisplayed()
        {
            return _attentionErrorElem.Displayed;
        }

    }
}
