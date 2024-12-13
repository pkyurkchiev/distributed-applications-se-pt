namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.API.Extensions;
    using GustoHub.Infrastructure.Attributes;

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

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var allOrders = await orderService.AllAsync();
            return Ok(allOrders);
        }
        [HttpGet("{dateTime}")]
        public async Task<IActionResult> GetOrderByDate(string dateTime)
        {
            var order = await orderService.GetByDateAsync(DateTime.Parse(dateTime));

            if (order == null)
            {
                return NotFound("Order not found!");
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] POSTOrderDto orderDto)
        {
            if (!await employeeService.ExistsByIdAsync(Guid.Parse(orderDto.EmployeeId)))
            {
                return NotFound("Employee not found!");
            }
            if (!await customerService.ExistsByIdAsync(Guid.Parse(orderDto.CustomerId)))
            {
                return NotFound("Customer not found!");
            }
            if (!await employeeService.IsEmployeeActiveAsync(Guid.Parse(orderDto.EmployeeId)))
            {
                return BadRequest("Employee is deactivated!");
            }

            string responseMessage = await orderService.AddAsync(orderDto);
            return Ok(new { message = responseMessage });
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(PUTOrderDto order, int id)
        {
            if (!await orderService.ExistsByIdAsync(id))
            {
                return NotFound("Order not found!");
            }

            string responseMessage = await orderService.UpdateAsync(order, id);
            return Ok(new { message = responseMessage });
        }

        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrder(int id)
        {
            if (!await orderService.ExistsByIdAsync(id))
            {
                return NotFound("Order not found!");
            }
            string responseMessage = await orderService.Remove(id);
            return Ok(new { message = responseMessage });
        }
    }
}
