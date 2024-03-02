using CrudTest.Feature.CustomerFeatures.Command.Add;
using CrudTest.Feature.CustomerFeatures.Command.Delete;
using CrudTest.Feature.CustomerFeatures.Query.GetCustomerList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly IMediator mediator;

    public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
    {
        _logger = logger;
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] AddCustomerCommandModel command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomers()
    {
        return Ok(await mediator.Send(new GetAllCustomersQueryModel()));
    }

    [HttpPut("customer/{id}")]
    public async Task<IActionResult> UpdateCustomer(long id, EditCustomerCommandModel command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpDelete("customer/{customerId}")]
    public async Task<IActionResult> DeleteCustomer(long customerId)
    {
        return Ok(await mediator.Send(new DeleteCustomerCommandModel { Id = customerId }));
    }
}