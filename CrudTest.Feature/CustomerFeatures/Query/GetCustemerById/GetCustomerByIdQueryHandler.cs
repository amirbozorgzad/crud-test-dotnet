using CrudTest.Core.Context.CustomerContext;
using CrudTest.Core.Context.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Feature.CustomerFeatures.Query.GetCustemerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQueryModel, Customer>
{
    private readonly ICustomerContext _customerContext;

    public GetCustomerByIdQueryHandler(ICustomerContext customerContext)
    {
        _customerContext = customerContext;
    }

    public async Task<Customer> Handle(GetCustomerByIdQueryModel request, CancellationToken cancellationToken)
    {
        Customer? customer = await _customerContext.Customer.FirstOrDefaultAsync(a => a.Id == request.Id);
        if (customer == null) return null;
        return customer;
    }
}