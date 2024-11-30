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
        private readonly IOrderService orderService;
        private readonly IDishService dishService;

        public OrderDishController(
            IOrderDishService orderDishService,
            IOrderService orderService,
            IDishService dishService)
        {
            this.orderDishService = orderDishService;
            this.orderService = orderService;
            this.dishService = dishService;
        }

        [HttpPost]
        public async Task<IActionResult> PostDishOrder
            ([FromQuery] int orderId,
            [FromQuery] int dishId,
            [FromQuery] int quantity)
        {
            if (!await orderService.ExistsByIdAsync(orderId))
            {
                return NotFound("Order not found!");
            }
            if (!await dishService.ExistsByIdAsync(dishId))
            {
                return NotFound("Dish not found!");
            }

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
