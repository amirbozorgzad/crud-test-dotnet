using MC2.CrudTest.Core.Contract;
using Mc2.CrudTest.Core.Contract.Customer;
using MC2.CrudTest.Core.Domain.Model;

namespace MC2.CrudTest.Service.Abstraction;

public interface ICustomerService
{
    ValueTask<ResultDto<List<Customer>?>> GetCustomers(long? id = null);
    ValueTask<ResultDto<string>> UpdateCustomer(CustomerDto customerDto, long id);
    ValueTask<ResultDto<string>> CreateCustomer(CustomerDto customerDto);
    ValueTask<ResultDto<string>> DeleteCustomer(long id);
}