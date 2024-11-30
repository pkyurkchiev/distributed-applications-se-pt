namespace GustoHub.API.Controllers
{
    using GustoHub.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;

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

            string responseMessage = await orderService.AddAsync(orderDto);
            return Ok(responseMessage);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var allOrders = await orderService.AllAsync();
            return Ok(allOrders);
        }
        [HttpGet("{orderName}")]
        public async Task<IActionResult> GetOrderByName(DateTime dateTime)
        {
            var order = await orderService.GetByDateAsync(dateTime);
            return Ok(order.OrderDate);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrder(int id)
        {
            return Ok(await orderService.Remove(id));
        }
    }
}
