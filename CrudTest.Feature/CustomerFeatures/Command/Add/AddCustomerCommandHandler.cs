using CrudTest.Core.Context.CustomerContext;
using CrudTest.Core.Context.Model;
using MediatR;

namespace CrudTest.Feature.CustomerFeatures.Command.Add;

public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommandModel, long>
{
    private readonly ICustomerContext _customerContext;

    public AddCustomerCommandHandler(ICustomerContext customerContext)
    {
        _customerContext = customerContext;
    }

    public async Task<long> Handle(AddCustomerCommandModel request, CancellationToken cancellationToken)
    {
        Customer customer = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            BankAccountNumber = request.BankAccountNumber,
            DateOfBirth = request.DateOfBirth
        };

        await _customerContext.Customer.AddAsync(customer, cancellationToken);
        await _customerContext.SaveChangesAsync();
        return customer.Id;
    }
}