using FluentAssertions;
using Mc2.CrudTest.AcceptanceTests.Drivers;
using MC2.CrudTest.Core.Contract;
using Mc2.CrudTest.Core.Contract.Customer;
using MC2.CrudTest.Core.Domain.Model;

namespace Mc2.CrudTest.AcceptanceTests.StepDefinitions;

[Binding]
public class CustomerManagementStepDefinitions(ICustomerDriver customerDriver)
{
    private readonly ICustomerDriver _customerDriver = customerDriver;
    private CustomerDto _customer;

    [Given(@"a customer with the following details:")]
    public async Task GivenACustomerWithTheFollowingDetails(Table table)
    {
        _customer = new CustomerDto
        {
            FirstName = table.Rows[0]["FirstName"],
            LastName = table.Rows[0]["LastName"],
            DateOfBirth = DateTime.Parse(table.Rows[0]["DateOfBirth"]),
            PhoneNumber = table.Rows[0]["PhoneNumber"],
            Email = table.Rows[0]["Email"],
            BankAccountNumber = table.Rows[0]["BankAccountNumber"]
        };
    }

    [When(@"the user adds the customer")]
    public async Task WhenTheUserAddsTheCustomer()
    {
        await _customerDriver.CreateCustomer(_customer);
    }

    [Then(@"the customer should be successfully added")]
    public async Task ThenTheCustomerShouldBeSuccessfullyAdded()
    {
        ResultDto<List<Customer>?> customers = await _customerDriver.GetCustomers();
        customers.Result.Count.Should().Be(1);
    }
}