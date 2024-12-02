namespace GustoHub.Services.Services
{
    using GustoHub.Data.Models;
    using GustoHub.Data.Common;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.GET;
    using GustoHub.Data.ViewModels.PUT;

    public class OrderDishService : IOrderDishService
    {
        private readonly IRepository repository;

        public OrderDishService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<string> AddDishToOrder(POSTOrderDishDto orderDishDto)
        {
            var orderDish = new OrderDish
            {
                OrderId = orderDishDto.OrderId,
                DishId = orderDishDto.DishId,
                Quantity = orderDishDto.Quantity
            };

            await repository.AddAsync(orderDish);
            await repository.SaveChangesAsync();

            return "Dish added successfully to Order!";    
        }
        public async Task<GETOrderDishDto?> GetOrderDishByIdAsync(int orderId, int dishId)
        {
            OrderDish? orderDish = await repository.AllAsReadOnly<OrderDish>()
                                 .Include(od => od.Order)
                                 .Include(od => od.Dish)
                                 .FirstOrDefaultAsync(od => od.OrderId == orderId && od.DishId == dishId);

            GETOrderDishDto orderDishDto = new GETOrderDishDto() 
            {
                OrderDate = orderDish.Order.OrderDate.ToString("f"),
                OrderCompletionDate = orderDish.Order.CompletionDate.HasValue
                     ? orderDish.Order.OrderDate.ToString("f")
                     : null,
                OrderCustomerId = orderDish.Order.CustomerId.ToString(),
                OrderEmployeeId = orderDish.Order.EmployeeId.ToString(),
                OrderTotalAmount = orderDish.Order.TotalAmount.ToString(),

                DishName = orderDish.Dish.Name,
                DishCategoryId = orderDish.Dish.CategoryId,
                DishPrice = orderDish.Dish.Price.ToString(),
            };

            return orderDishDto;
        }
        public async Task<IEnumerable<Dish>> GetDishesForOrder(int orderId)
        {
            Order order = await repository.GetByIdAsync<Order>(orderId);

            var dishes = await repository
                    .AllAsReadOnly<OrderDish>()
                    .Where(od => od.OrderId == orderId)
                    .Select(od => od.Dish)
                    .ToListAsync();

            return dishes;

        }
    }
}
