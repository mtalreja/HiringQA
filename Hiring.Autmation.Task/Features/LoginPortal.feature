@browser

Feature: LoginPortal

Scenario: UBS Client Portal Login
	Given the user starts browsing the UBS website
	When the client enters wrong email address '123@test' and password '123' 
	Then error message is displayed