namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Infrastructure.Attributes;
    using System;
    using System.Threading.Tasks;

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

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>A list of all customers.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var allCustomers = await customerService.AllAsync();
            return Ok(allCustomers);
        }

        /// <summary>
        /// Retrieves a customer by their name.
        /// </summary>
        /// <param name="customerName">The name of the customer.</param>
        /// <returns>The customer if found, otherwise a 404 response.</returns>
        [HttpGet("{customerName}")]
        public async Task<IActionResult> GetCustomerByName(string customerName)
        {
            var customer = await customerService.GetByNameAsync(customerName);

            if (customer == null)
            {
                return NotFound(new { message = "Customer not found!" });
            }

            return Ok(customer);
        }

        /// <summary>
        /// Creates a new customer (Admin Only).
        /// </summary>
        /// <param name="customerDto">The customer data to be added.</param>
        /// <returns>A success message.</returns>
        [AuthorizeRole("Admin")]
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] POSTCustomerDto customerDto)
        {
            string responseMessage = await customerService.AddAsync(customerDto);
            return Ok(new { message = responseMessage });
        }

        /// <summary>
        /// Updates an existing customer by their ID (Admin Only).
        /// </summary>
        /// <param name="customer">The updated customer data.</param>
        /// <param name="id">The ID of the customer to update.</param>
        /// <returns>A success message or a 404 response if not found.</returns>
        [AuthorizeRole("Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(PUTCustomerDto customer, string id)
        {
            if (!await customerService.ExistsByIdAsync(Guid.Parse(id)))
            {
                return NotFound(new { message = "Customer not found!" });
            }

            string responseMessage = await customerService.UpdateAsync(customer, id);
            return Ok(new { message = responseMessage });
        }

        /// <summary>
        /// Deletes a customer by their ID (Admin Only).
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>A success message or a 404 response if not found.</returns>
        [AuthorizeRole("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCustomer(string id)
        {
            if (!await customerService.ExistsByIdAsync(Guid.Parse(id)))
            {
                return NotFound(new { message = "Customer not found!" });
            }

            string responseMessage = await customerService.Remove(Guid.Parse(id));
            return Ok(new { message = responseMessage });
        }
    }
}
