using CrudTest.Core.Context.Model;
using CrudTest.Feature.CustomerFeatures.Command.Add;

namespace Mc2.CrudTest.AcceptanceTests.Drivers;

public interface ICustomerDriver
{
    Task CreateCustomer(AddCustomerCommandModel customer);
    Task UpdateCustomer(EditCustomerCommandModel customer);
    Task<Customer> GetCustomerById(long id);
    Task<IEnumerable<Customer>> GetCustomers();
    Task DeleteCustomer(long id);
}