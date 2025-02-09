namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Infrastructure.Attributes;
    using System;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IEmployeeService employeeService;
        private readonly ICustomerService customerService;

        public OrderController(
            IOrderService orderService,
            IEmployeeService employeeService,
            ICustomerService customerService)
        {
            this.orderService = orderService;
            this.employeeService = employeeService;
            this.customerService = customerService;
        }

        /// <summary>
        /// Retrieves all orders. (Admin Only, API Key Required)
        /// </summary>
        /// <returns>A list of all orders.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var allOrders = await orderService.AllAsync();
            return Ok(allOrders);
        }

        /// <summary>
        /// Retrieves an order by its date. 
        /// </summary>
        /// <param name="dateTime">The date of the order (in string format).</param>
        /// <returns>The order details if found, otherwise a 404 response.</returns>
        [HttpGet("{dateTime}")]
        public async Task<IActionResult> GetOrderByDate(string dateTime)
        {
            if (!DateTime.TryParse(dateTime, out var parsedDate))
            {
                return BadRequest(new { message = "Invalid date format!" });
            }

            var order = await orderService.GetByDateAsync(parsedDate);

            if (order == null)
            {
                return NotFound(new { message = "Order not found!" });
            }

            return Ok(order);
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="orderDto">The order data.</param>
        /// <returns>A success message or an error response if validation fails.</returns>
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] POSTOrderDto orderDto)
        {
            if (!Guid.TryParse(orderDto.EmployeeId, out var employeeId) ||
                !Guid.TryParse(orderDto.CustomerId, out var customerId))
            {
                return BadRequest(new { message = "Invalid EmployeeId or CustomerId format!" });
            }

            if (!await employeeService.ExistsByIdAsync(employeeId))
            {
                return NotFound(new { message = "Employee not found!" });
            }

            if (!await customerService.ExistsByIdAsync(customerId))
            {
                return NotFound(new { message = "Customer not found!" });
            }

            if (!await employeeService.IsEmployeeActiveAsync(employeeId))
            {
                return BadRequest(new { message = "Employee is deactivated!" });
            }

            string responseMessage = await orderService.AddAsync(orderDto);
            return Ok(new { message = responseMessage });
        }

        /// <summary>
        /// Updates an existing order by its ID. (Admin Only, API Key Required)
        /// </summary>
        /// <param name="order">The updated order data.</param>
        /// <param name="id">The ID of the order to update.</param>
        /// <returns>A success message or a 404 response if the order is not found.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromBody] PUTOrderDto order, int id)
        {
            if (!await orderService.ExistsByIdAsync(id))
            {
                return NotFound(new { message = "Order not found!" });
            }

            string responseMessage = await orderService.UpdateAsync(order, id);
            return Ok(new { message = responseMessage });
        }

        /// <summary>
        /// Deletes an order by its ID. (Admin Only, API Key Required)
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>A success message or a 404 response if the order is not found.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrder(int id)
        {
            if (!await orderService.ExistsByIdAsync(id))
            {
                return NotFound(new { message = "Order not found!" });
            }

            string responseMessage = await orderService.Remove(id);
            return Ok(new { message = responseMessage });
        }
    }
}
