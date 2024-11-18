namespace GustoHub.Services.Services
{
    using GustoHub.Data.Common;
    using GustoHub.Data.Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class OrderService : IOrderService
    {
        private readonly IRepository repository;

        public OrderService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> AddAsync(Order order)
        {
            if (!await ExistsByIdAsync(order.Id))
            {
                await repository.AddAsync(order);
                await repository.SaveChangesAsync();

                return "Order added Successfully!";
            }

            return "Order already exists!";
        }

        public async Task<IEnumerable<Order>> AllAsync()
        {
            return await repository.AllAsync<Order>();
        }

        public async Task<bool> ExistsByIdAsync(int orderId)
        {
            return await repository.AllAsReadOnly<Order>().AnyAsync(o => o.Id == orderId);
        }

        public async Task<Order> GetByIdAsync(int orderId)
        {
            return await repository.AllAsReadOnly<Order>().FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order> GetByDateAsync(DateTime date)
        {
            return await repository.AllAsReadOnly<Order>().FirstOrDefaultAsync(o => o.OrderDate == date);
        }

        public async Task<string> Remove(int id)
        {
            if (await ExistsByIdAsync(id))
            {
                await repository.RemoveAsync<Order>(id);
                await repository.SaveChangesAsync();
                return "Order removed successfully!";
            }
            return "Order doesn't exists!";
        }
    }
}
