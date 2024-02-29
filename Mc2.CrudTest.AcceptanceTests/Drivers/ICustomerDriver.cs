using MC2.CrudTest.Core.Contract;
using Mc2.CrudTest.Core.Contract.Customer;
using MC2.CrudTest.Core.Domain.Model;

namespace Mc2.CrudTest.AcceptanceTests.Drivers;

public interface ICustomerDriver
{
    Task CreateCustomer(CustomerDto customer);
    Task UpdateCustomer(long id, CustomerDto customer);
    Task<ResultDto<List<Customer>?>> GetCustomers();
    Task DeleteCustomer(long id);
}