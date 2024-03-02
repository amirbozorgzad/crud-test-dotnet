using CrudTest.Core.Context.CustomerContext;
using CrudTest.Core.Context.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Feature.CustomerFeatures.Command.Add;

public class EditCustomerCommandHandler : IRequestHandler<EditCustomerCommandModel, long>
{
    private readonly ICustomerContext _customerContext;

    public EditCustomerCommandHandler(ICustomerContext customerContext)
    {
        _customerContext = customerContext;
    }

    public async Task<long> Handle(EditCustomerCommandModel request, CancellationToken cancellationToken)
    {
        Customer? customer =
            await _customerContext.Customer.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (customer == null) return default;

        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.Email = request.Email;
        customer.PhoneNumber = request.PhoneNumber;
        customer.DateOfBirth = request.DateOfBirth;
        customer.BankAccountNumber = request.BankAccountNumber;

        _customerContext.Customer.Update(customer);
        await _customerContext.SaveChangesAsync();
        return customer.Id;
    }
}