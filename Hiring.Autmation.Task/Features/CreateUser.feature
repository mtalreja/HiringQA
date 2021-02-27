@browser

Feature: CreateUser

@mytag
Scenario: Create User for Job Search with same security question and verify error is thrown or not
	Given the user starts browsing the UBS website
	When the user navigates to 'Careers' section and chooses 'Career Comeback'
	And selects apply now on Career Comeback Page
	And selects Sign in tab with Don't have an account yet as an option
	And enters new user creation details
	| Email Address        | Password  | Security Question 1                    | Security Question 1 Ans | Security Question 2                   | Security Question 2 Ans | Security Question 3                   | Security Question 3 Ans |
	| machine123@gmail.com | test@1231 | What is the name of your first school? | Schoole                 | Where is your favorite vacation spot? | US                      | Where is your favorite vacation spot? | Spot                    |
	And proceed with continue
	Then duplicate security error is thrown