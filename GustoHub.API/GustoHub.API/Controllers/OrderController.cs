namespace GustoHub.API.Controllers
{
    using GustoHub.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] Order order)
        {
            string responseMessage = await orderService.AddAsync(order);
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
