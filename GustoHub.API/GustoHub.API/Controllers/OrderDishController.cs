using GustoHub.Data.Common;
using GustoHub.Data.Models;
using GustoHub.Services.Interfaces;
using GustoHub.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GustoHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDishController : ControllerBase
    {
        private readonly IOrderDishService orderDishService;

        public OrderDishController(IOrderDishService orderDishService)
        {
            this.orderDishService = orderDishService;
        }

        [HttpPost]
        public async Task<IActionResult> PostDishOrder
            ([FromQuery] int orderId,
            [FromQuery] int dishId,
            [FromQuery] int quantity)
        {
            string responseMessage = await orderDishService.AddDishToOrder(orderId, dishId, quantity);
            return Ok(responseMessage);
        }
        [HttpGet("all/{orderId}")]
        public async Task<IActionResult> GetAllDishOrders(int orderId)
        {
            var allCustomers = await orderDishService.GetDishesForOrder(orderId);
            return Ok(allCustomers);
        }
    }
}
