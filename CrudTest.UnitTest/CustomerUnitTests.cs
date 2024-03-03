using CrudTest.Core.Context.Model;
using CrudTest.Feature.CustomerFeatures.Command.Add;
using CrudTest.Feature.CustomerFeatures.Query.GetCustemerById;
using CrudTest.Feature.CustomerFeatures.Query.GetCustomerList;
using Mc2.CrudTest.AcceptanceTests.Drivers;
using MediatR;
using Moq;

namespace Mc2.CrudTest.UnitTests;

[TestFixture]
public class CustomerDriverTests
{
    [Test]
    public void CreateCustomer_WhenCalled_ShouldInvokeMediatorSend()
    {
        // Arrange
        Mock<IMediator> mockMediator = new();
        CustomerDriver customerDriver = new(mockMediator.Object);
        AddCustomerCommandModel customer = new();

        // Act
        customerDriver.CreateCustomer(customer).Wait();

        // Assert
        mockMediator.Setup(m => m.Send(It.IsAny<object>(), default)).Verifiable();
    }

    [Test]
    public async Task GetCustomers_WhenCalled_ShouldReturnListOfCustomers()
    {
        // Arrange
        List<Customer> expectedCustomers = new()
        {
            new Customer { Id = 1, FirstName = "John", LastName = "Doe" },
            new Customer { Id = 2, FirstName = "Jane", LastName = "Doe" }
        };

        Mock<IMediator> mockMediator = new();
        mockMediator.Setup(m => m.Send(It.IsAny<GetAllCustomersQueryModel>(), default))
            .ReturnsAsync(expectedCustomers);

        CustomerDriver customerDriver = new(mockMediator.Object);

        // Act
        IEnumerable<Customer> result = await customerDriver.GetCustomers();

        // Assert
        Assert.AreEqual(expectedCustomers, result);
    }

    [Test]
    public async Task GetCustomerById_WhenCalled_ShouldReturnCustomer()
    {
        // Arrange
        Customer expectedCustomer = new() { Id = 1, FirstName = "John", LastName = "Doe" };

        Mock<IMediator> mockMediator = new();
        mockMediator.Setup(m => m.Send(It.IsAny<GetCustomerByIdQueryModel>(), default))
            .ReturnsAsync(expectedCustomer);

        CustomerDriver customerDriver = new(mockMediator.Object);
        long customerId = 1;

        // Act
        Customer result = await customerDriver.GetCustomerById(customerId);

        // Assert
        Assert.AreEqual(expectedCustomer, result);
    }

    [Test]
    public async Task UpdateCustomer_WhenCalled_ShouldReturnNumber()
    {
        // Arrange
        Mock<IMediator> mockMediator = new();
        CustomerDriver customerDriver = new(mockMediator.Object);
        EditCustomerCommandModel customer = new();

        // Mock the behavior of IMediator
        mockMediator.Setup(m => m.Send(It.IsAny<EditCustomerCommandModel>(), default))
            .ReturnsAsync(1); // Assuming the update was successful

        // Act
        long result = await customerDriver.UpdateCustomer(customer);

        // Assert
        Assert.IsTrue(result > 0);
    }

    [Test]
    public void DeleteCustomer_WhenCalled_ShouldInvokeMediatorSendWithCorrectId()
    {
        // Arrange
        Mock<IMediator> mockMediator = new();
        CustomerDriver customerDriver = new(mockMediator.Object);
        long customerId = 123;

        // Act
        customerDriver.DeleteCustomer(customerId).Wait();

        // Assert
        mockMediator.Setup(m => m.Send(It.IsAny<object>(), default)).Verifiable();
    }
}