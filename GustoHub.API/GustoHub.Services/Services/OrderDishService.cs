namespace GustoHub.Services.Services
{
    using GustoHub.Data.Models;
    using GustoHub.Data.Common;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class OrderDishService : IOrderDishService
    {
        private readonly IRepository repository;

        public OrderDishService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<string> AddDishToOrder(int orderId, int dishId, int quantity)
        {
            Order order = await repository.GetByIdAsync<Order>(orderId);

            if (order != null) 
            {
                OrderDish orderDish = new OrderDish() 
                {
                    OrderId = orderId,
                    DishId = dishId,
                    Quantity = quantity
                };

                await repository.AddAsync(orderDish);
                await repository.SaveChangesAsync();

                return "Dish added successfully to Order!";
            }

            return "Order doesn't exist!";
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
