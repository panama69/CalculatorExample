#Auto generated NGA revision tag
@TID12001REV0.2.0
Feature: Calc Sum 
	In order to avoid silly mistakes 
    As a math idiot I want to be told the sum of two numbers

Scenario: Add two numbers
Given I have entered 5 into the calculator 
And I press "Add" 
And I have entered 7 into the calculator 
When I press equals 
Then the result should be 13 on the screen
