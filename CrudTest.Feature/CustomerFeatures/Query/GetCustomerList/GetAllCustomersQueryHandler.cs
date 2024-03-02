using CrudTest.Core.Context.CustomerContext;
using CrudTest.Core.Context.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Feature.CustomerFeatures.Query.GetCustomerList;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQueryModel, IEnumerable<Customer>>
{
    private readonly ICustomerContext _customerContext;

    public GetAllCustomersQueryHandler(ICustomerContext customerContext)
    {
        _customerContext = customerContext;
    }

    public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQueryModel request,
        CancellationToken cancellationToken)
    {
        List<Customer>? customer = await _customerContext.Customer.ToListAsync();
        if (customer == null) return null;
        return customer;
    }
}