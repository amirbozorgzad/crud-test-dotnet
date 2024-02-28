using Core.Contract;
using Mc2.CrudTest.Core.Contract.Customer;
using MC2.CrudTest.Core.Domain.Model;

namespace Service.Abstraction;

public interface ICustomerService
{
    ValueTask<ResultDto<List<Customer>?>> GetCustomers(long? id);
    ValueTask<ResultDto<(string message, long id)>> UpdateCustomer(CustomerDto customerDto);
    ValueTask<ResultDto<(string message, long id)>> CreateCustomer(CustomerDto customerDto);
    ValueTask<ResultDto<string>> DeleteCustomer(long id);
}