@browser

Feature: UBS Tests

Scenario Outline: Change Of Domicile
		Given the user starts browsing the UBS website
		When the user changes '<region>' with '<country>'
		Then the user sees the domicile is updated to '<country>'
		
		Examples: 
		| region       | country |
		| Asia Pacific | India   |
		| Europe       | Germany |

				