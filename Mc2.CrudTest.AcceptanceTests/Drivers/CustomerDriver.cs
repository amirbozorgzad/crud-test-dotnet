using MC2.CrudTest.Core.Contract;
using Mc2.CrudTest.Core.Contract.Customer;
using MC2.CrudTest.Core.Domain.Model;
using MC2.CrudTest.Service.Abstraction;

namespace Mc2.CrudTest.AcceptanceTests.Drivers;

public class CustomerDriver(ICustomerService customerService) : ICustomerDriver
{
    private readonly ICustomerService _customerService = customerService;

    public async Task CreateCustomer(CustomerDto customer)
    {
        await _customerService.CreateCustomer(customer);
    }

    public async Task DeleteCustomer(long id)
    {
        await _customerService.DeleteCustomer(id);
    }

    public async Task<ResultDto<List<Customer>?>> GetCustomers()
    {
        return await _customerService.GetCustomers();
    }

    public async Task UpdateCustomer(long id, CustomerDto customer)
    {
        await _customerService.UpdateCustomer(customer, id);
    }
}