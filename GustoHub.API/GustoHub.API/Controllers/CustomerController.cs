namespace GustoHub.API.Controllers
{
    using GustoHub.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Services.Services;

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
            var customer = await customerService.GetByNameAsync(customerName);
            return Ok(customerName);
        }
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] POSTCustomerDto customerDto)
        {
            string responseMessage = await customerService.AddAsync(customerDto);
            return Ok(responseMessage);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCusomer(PUTCustomerDto customer, string id)
        {
            return Ok(await customerService.UpdateAsync(customer, id));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCustomer(Guid id)
        {
            return Ok(await customerService.Remove(id));
        }
    }
}
