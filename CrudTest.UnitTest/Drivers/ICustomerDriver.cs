using CrudTest.Core.Context.Model;
using CrudTest.Feature.CustomerFeatures.Command.Add;

namespace Mc2.CrudTest.AcceptanceTests.Drivers;

public interface ICustomerDriver
{
    Task<long> CreateCustomer(AddCustomerCommandModel customer);
    Task<long> UpdateCustomer(EditCustomerCommandModel customer);
    Task<Customer> GetCustomerById(long id);
    Task<IEnumerable<Customer>> GetCustomers();
    Task<long> DeleteCustomer(long id);
}