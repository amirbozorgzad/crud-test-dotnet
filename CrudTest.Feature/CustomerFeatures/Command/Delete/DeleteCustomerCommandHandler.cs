using CrudTest.Core.Context.CustomerContext;
using CrudTest.Core.Context.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Feature.CustomerFeatures.Command.Delete;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommandModel, long>
{
    private readonly ICustomerContext _customerContext;

    public DeleteCustomerCommandHandler(ICustomerContext customerContext)
    {
        _customerContext = customerContext;
    }

    public async Task<long> Handle(DeleteCustomerCommandModel request, CancellationToken cancellationToken)
    {
        Customer? customer = await _customerContext.Customer.Where(c => c.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (customer == null) return default;
        _customerContext.Customer.Remove(customer);
        await _customerContext.SaveChangesAsync();
        return customer.Id;
    }
}