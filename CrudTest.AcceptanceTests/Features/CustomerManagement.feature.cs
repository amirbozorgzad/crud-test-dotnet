using CrudTest.Core.Context.Model;
using CrudTest.Feature.CustomerFeatures.Command.Add;
using Mc2.CrudTest.AcceptanceTests.Drivers;
using NUnit.Framework;

namespace Mc2.CrudTest.AcceptanceTests.Features;

[Binding]
public class CustomerManagementSteps
{
    private readonly CustomerDriver _customerDriver;
    private long _customerId;

    public CustomerManagementSteps(CustomerDriver customerDriver)
    {
        _customerDriver = customerDriver;
    }

    [Given(@"a customer with the following details:")]
    public void GivenACustomerWithTheFollowingDetails(Table table)
    {
        TableRow? customerDetails = table.Rows[0];
        string? firstName = customerDetails["FirstName"];
        string? lastName = customerDetails["LastName"];
        DateTime dateOfBirth = DateTime.Parse(customerDetails["DateOfBirth"]);
        string? phoneNumber = customerDetails["PhoneNumber"];
        string? email = customerDetails["Email"];
        string? bankAccountNumber = customerDetails["BankAccountNumber"];

        AddCustomerCommandModel newCustomer = new()
        {
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            PhoneNumber = phoneNumber,
            Email = email,
            BankAccountNumber = bankAccountNumber
        };

        _customerDriver.CreateCustomer(newCustomer).Wait();
    }

    [When(@"the user adds the customer")]
    public void WhenTheUserAddsTheCustomer()
    {
    }

    [Then(@"the customer should be successfully added")]
    public async void ThenTheCustomerShouldBeSuccessfullyAdded()
    {
        Customer addedCustomer = await _customerDriver.GetCustomerById(_customerId);
        Assert.IsTrue(addedCustomer.Id > 0);
        Assert.IsNotNull(addedCustomer, "Customer was added successfully.");
    }

    [Given(@"a customer with the following details:")]
    public void GivenACustomerWithTheFollowingDetailsEdit(Table table)
    {
        TableRow? customerDetails = table.Rows[0];
        _customerId = long.Parse(customerDetails["Id"]);

        EditCustomerCommandModel editedCustomer = new()
        {
            Id = _customerId,
            FirstName = customerDetails["FirstName"],
            LastName = customerDetails["LastName"],
            DateOfBirth = DateTime.Parse(customerDetails["DateOfBirth"]),
            PhoneNumber = customerDetails["PhoneNumber"],
            Email = customerDetails["Email"],
            BankAccountNumber = customerDetails["BankAccountNumber"]
        };

        _customerDriver.UpdateCustomer(editedCustomer).Wait();
    }

    [When(@"the user edit the customer")]
    public void WhenTheUserEditTheCustomer()
    {
    }

    [Then(@"the customer should be successfully edited and show the customer id")]
    public async void ThenTheCustomerShouldBeSuccessfullyEditedAndShowTheCustomerId()
    {
        Customer addedCustomer = await _customerDriver.GetCustomerById(_customerId);
        Assert.IsTrue(addedCustomer.Id > 0);
        Assert.IsNotNull(addedCustomer, "Customer was edited successfully.");
    }

    [Given(@"a customer with the following details:")]
    public void GivenACustomerWithTheFollowingDetailsDelete(Table table)
    {
        TableRow? customerDetails = table.Rows[0];
        _customerId = long.Parse(customerDetails["Id"]);

        _customerDriver.DeleteCustomer(_customerId).Wait();
    }

    [When(@"the user delete the customer")]
    public void WhenTheUserDeleteTheCustomer()
    {
    }

    [Then(@"the customer should be successfully deleted")]
    public async void ThenTheCustomerShouldBeSuccessfullyDeleted()
    {
        Customer addedCustomer = await _customerDriver.GetCustomerById(_customerId);
        Assert.IsTrue(addedCustomer.Id == 0);
        Assert.IsNull(addedCustomer, "Customer was deleted successfully.");
    }
}