using CrudTest.Core.Context.Model;
using CrudTest.Feature.CustomerFeatures.Command.Add;
using CrudTest.Feature.CustomerFeatures.Command.Delete;
using CrudTest.Feature.CustomerFeatures.Query.GetCustemerById;
using CrudTest.Feature.CustomerFeatures.Query.GetCustomerList;
using MediatR;

namespace Mc2.CrudTest.AcceptanceTests.Drivers;

public class CustomerDriver(IMediator mediator) : ICustomerDriver
{
    private readonly IMediator _mediator = mediator;

    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        return await _mediator.Send(new GetAllCustomersQueryModel());
    }

    public async Task CreateCustomer(AddCustomerCommandModel customer)
    {
        await _mediator.Send(customer);
    }

    public async Task UpdateCustomer(EditCustomerCommandModel customer)
    {
        await _mediator.Send(customer);
    }

    public async Task DeleteCustomer(long id)
    {
        await mediator.Send(new DeleteCustomerCommandModel { Id = id });
    }


    public async Task<Customer> GetCustomerById(long id)
    {
        return await _mediator.Send(new GetCustomerByIdQueryModel { Id = id });
    }
}