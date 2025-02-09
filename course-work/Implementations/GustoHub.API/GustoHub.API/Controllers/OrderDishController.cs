namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Infrastructure.Attributes;
    using System.Threading.Tasks;

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

        /// <summary>
        /// Retrieves all order-dish relationships. (Admin Only, API Key Required)
        /// </summary>
        /// <returns>A list of all dishes linked to orders.</returns>
        [AuthorizeRole("Admin")]
        [APIKeyRequired]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrderDishes()
        {
            var allOrderDishes = await orderDishService.AllAsync();
            return Ok(allOrderDishes);
        }

        /// <summary>
        /// Retrieves all dishes associated with a specific order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>A list of dishes linked to the specified order.</returns>
        [HttpGet("allDishesBy/{orderId:int}")]
        public async Task<IActionResult> GetAllDishesForOrder(int orderId)
        {
            if (!await orderService.ExistsByIdAsync(orderId))
            {
                return NotFound(new { message = "Order not found!" });
            }

            var allDishesByOrder = await orderDishService.GetDishesForOrder(orderId);
            return Ok(allDishesByOrder);
        }

        /// <summary>
        /// Retrieves a specific dish from an order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <param name="dishId">The ID of the dish.</param>
        /// <returns>The order-dish relationship if found, otherwise a 404 response.</returns>
        [HttpGet("{orderId:int}/{dishId:int}")]
        public async Task<IActionResult> GetById(int orderId, int dishId)
        {
            if (!await orderService.ExistsByIdAsync(orderId))
            {
                return NotFound(new { message = "Order not found!" });
            }

            if (!await dishService.ExistsByIdAsync(dishId))
            {
                return NotFound(new { message = "Dish not found!" });
            }

            var orderDish = await orderDishService.GetOrderDishByIdAsync(orderId, dishId);
            if (orderDish == null)
            {
                return NotFound(new { message = "Dish for order not found!" });
            }

            return Ok(orderDish);
        }

        /// <summary>
        /// Adds a dish to an order.
        /// </summary>
        /// <param name="orderDishDto">The data for the dish and order association.</param>
        /// <returns>A success message or an error response if validation fails.</returns>
        [HttpPost]
        public async Task<IActionResult> PostOrderDish([FromBody] POSTOrderDishDto orderDishDto)
        {
            if (!await orderService.ExistsByIdAsync(orderDishDto.OrderId))
            {
                return NotFound(new { message = "Order not found!" });
            }

            if (!await dishService.ExistsByIdAsync(orderDishDto.DishId))
            {
                return NotFound(new { message = "Dish not found!" });
            }

            string responseMessage = await orderDishService.AddDishToOrderAsync(orderDishDto);
            return Ok(new { message = responseMessage });
        }
    }
}
