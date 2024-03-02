using CrudTest.Core.Context.Model;
using MediatR;

namespace CrudTest.Feature.CustomerFeatures.Query.GetCustomerList;

public class GetAllCustomersQueryModel : IRequest<IEnumerable<Customer>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string BankAccountNumber { get; set; }
}