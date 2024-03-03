Feature: Customer Management

As a an operator I wish to be able to Create, Update, Delete customers and list all customers
	
Scenario: Add A New Customer
	Given a customer with the following details:
		| FirstName | LastName | DateOfBirth | PhoneNumber | Email                | BankAccountNumber |
		| John      | Doe      | 1985-05-13  | 09133202668 | john.doe@example.com | 1234567890        |
	When the user adds the customer
	Then the customer should be successfully added
	
Scenario: Edit A New Customer
   	Given a customer with the following details:
   		| Id | FirstName  | LastName     | DateOfBirth | PhoneNumber | Email                       | BankAccountNumber |
   		| 1  | John-edited| Doe-edited   | 1985-05-14  | 09133202669 | john.doe-edited@example.com | 1234567891        |
   	When the user edit the customer
   	Then the customer should be successfully edited and show the customer id
   	
Scenario: Delete A New Customer
   	Given a customer with the following details:
   		| Id |
   		| 1  |
   	When the user delete the customer
   	Then the customer should be successfully deleted   	