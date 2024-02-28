using Mc2.CrudTest.Core.Contract.Customer;
using MC2.CrudTest.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Controllers;

[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(GroupName = "CustomerAreaV1")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto dto)
    {
        return Ok(await _customerService.CreateCustomer(dto));
    }

    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomers()
    {
        return Ok(await _customerService.GetCustomers());
    }

    [HttpPut("customer/{id}")]
    public async Task<IActionResult> UpdateCustomer(long id, [FromBody] CustomerDto dto)
    {
        return Ok(await _customerService.UpdateCustomer(dto, id));
    }

    [HttpDelete("customer/{customerId}")]
    public async Task<IActionResult> DeleteCustomer(int customerId)
    {
        return Ok(await _customerService.DeleteCustomer(customerId));
    }
}