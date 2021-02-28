using Hiring.Autmation.Task.PageObjects;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Hiring.Autmation.Task.Modals;
using System.Collections.Generic;
using NUnit.Framework;

namespace Hiring.Autmation.Task.StepsDefination
{
    [Binding]
    public class CreateUserSteps
    {
        private UBSHomePage _ubsHomePage;
        private CareerComebackPage _careerComebackPage;
        private JobSearchPage _jobSearchPage;
        private CreateNewAccountPage _createNewAccountPage;
        private PrivacyPolicyPage _privacyPolicyPage;

        public CreateUserSteps(IWebDriver driver)
        {
            _ubsHomePage = new UBSHomePage(driver);
            _careerComebackPage = new CareerComebackPage(driver);
            _jobSearchPage = new JobSearchPage(driver);
            _privacyPolicyPage = new PrivacyPolicyPage(driver);
            _createNewAccountPage = new CreateNewAccountPage(driver);

        }

        [When(@"the user navigates to '(.*)' section and chooses '(.*)'")]
        public void WhenTheUserNavigatesToSectionAndChooses(string parentName, string childText)
        {
            _ubsHomePage.NavigateToLink(parentName, childText);
        }

        [When(@"selects apply now on Career Comeback Page")]
        public void WhenSelectsApplyNowOnCareerComebackPage()
        {
            _ubsHomePage.ClickPrivacyOkBtn();
            _careerComebackPage.ClickApplyNowButton();
        }

        [When(@"selects Sign in tab with Don't have an account yet as an option")]
        public void WhenSelectsSignInTabWithDonTHaveAnAccountYetAsAnOption()
        {
            _jobSearchPage.ClickSignIn();
            _jobSearchPage.ClickDontHaveAnAccountLocator();
            _privacyPolicyPage.AggreePrivacyPolicy();
        }

        [When(@"enters new user creation details")]
        public void WhenEntersNewUserCreationDetails(Table userTable)
        {
            var user = new User();
            user.QuestionAnswers = new List<Tuple<string, string>>();
            foreach (var data in userTable.Rows)
            {
                user.EmailAddress = data["Email Address"];
                user.Password = data["Password"];
                var securityQuestionOne = data["Security Question 1"];
                var securityQuestionOneAns = data["Security Question 1 Ans"];
                var securityQuestionTwo = data["Security Question 2"];
                var securityQuestionTwoAns = data["Security Question 2 Ans"];
                var securityQuestionThree = data["Security Question 3"];
                var securityQuestionThreeAns = data["Security Question 3 Ans"];
                user.QuestionAnswers.Add(Tuple.Create(securityQuestionOne, securityQuestionOneAns));
                user.QuestionAnswers.Add(Tuple.Create(securityQuestionTwo, securityQuestionTwoAns));
                user.QuestionAnswers.Add(Tuple.Create(securityQuestionThree, securityQuestionThreeAns));
            }
            _createNewAccountPage.EnterNewUserDetails(user);
        }

        [When(@"proceed with continue")]
        public void WhenProceedWithContinue()
        {
            _createNewAccountPage.ClickContinueBtn();
        }

        [Then(@"duplicate security error is thrown")]
        public void ThenDuplicateSecurityErrorIsThrown()
        {
            Assert.IsTrue(_createNewAccountPage.IsAttentionErrorDisplayed());
        }

    }
}
