namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Infrastructure.Attributes;

    [APIKeyRequired]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var allCustomers = await customerService.AllAsync();
            return Ok(allCustomers);
        }

        [HttpGet("{customerName}")]
        public async Task<IActionResult> GetCustomerByName(string customerName)
        {
            var customer
                = await customerService.GetByNameAsync(customerName);

            if (customer == null)
            {
                return NotFound(new {message = "Customer not found!" });
            }

            return Ok(customer);
        }

        [AuthorizeRole("Admin")]
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] POSTCustomerDto customerDto)
        {
            string responseMessage = await customerService.AddAsync(customerDto);
            return Ok(new { message = responseMessage });
        }

        [AuthorizeRole("Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCusomer(PUTCustomerDto customer, string id)
        {
            if (!await customerService.ExistsByIdAsync(Guid.Parse(id)))
            {
                return NotFound("Cutomer not found!");
            }

            string responseMessage = await customerService.UpdateAsync(customer, id);

            return Ok(new { message = responseMessage });
        }

        [AuthorizeRole("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCustomer(string id)
        {
            if (!await customerService.ExistsByIdAsync(Guid.Parse(id)))
            {
                return NotFound("Cutomer not found!");
            }

            string responseMessage = await customerService.Remove(Guid.Parse(id));

            return Ok(new { message = responseMessage });
        }
    }
}
