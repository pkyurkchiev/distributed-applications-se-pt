namespace GustoHub.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using GustoHub.Services.Interfaces;
    using GustoHub.Data.ViewModels.POST;

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

        [HttpGet("all/{orderId}")]
        public async Task<IActionResult> GetAllDishesForOrder(int orderId)
        {
            var allCustomers = await orderDishService.GetDishesForOrder(orderId);
            return Ok(allCustomers);
        }
        [HttpGet("{orderId:int}/{dishId:int}")]
        public async Task<IActionResult> GetById(int orderId, int dishId)
        {
            var orderDish = await orderDishService.GetOrderDishByIdAsync(orderId, dishId);
            if (orderDish == null) 
            {
                return NotFound("Dish for order not found!");
            }
            return Ok(orderDish);
        }
        [HttpPost]
        public async Task<IActionResult> PostOrderDish([FromBody] POSTOrderDishDto orderDishDto)
        {
            string responseMessage = await orderDishService.AddDishToOrder(orderDishDto);
            return Ok(responseMessage);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrderDish([FromBody] POSTOrderDishDto orderDishDto)
        {
            string responseMessage = await orderDishService.AddDishToOrder(orderDishDto);
            return Ok(responseMessage);
        }
    }
}
