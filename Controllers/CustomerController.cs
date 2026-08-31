using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("customers")]
    public async Task<ActionResult<List<Customer>>> GetAllCustomer()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        if (customers == null)
        {
            return NotFound();
        }
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomerById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerDto dto)
    {
        var customer = await _customerService.CreateCustomerAsync(dto);
        return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Customer>> UpdateCustomer(int id, CustomerDto dto)
    {
        var updatedCustomer = await _customerService.UpdateCustomerAsync(id, dto);
        if (updatedCustomer == null)
        {
            return NotFound();
        }
        return Ok(updatedCustomer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerById(int id)
    {
        var deleted = await _customerService.DeleteCustomerAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}