namespace GustoHub.Services.Services
{
    using GustoHub.Data.Common;
    using GustoHub.Data.Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using GustoHub.Data.ViewModels;
    using System.Globalization;

    public class OrderService : IOrderService
    {
        private readonly IRepository repository;

        public OrderService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<string> AddAsync(POSTOrderDto orderDto)
        {
            Order order = new Order() 
            {
                OrderDate = DateTime.ParseExact(orderDto.OrderDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                CompletionDate = string.IsNullOrWhiteSpace(orderDto.CompletionDate)
                     ? (DateTime?)null
                     : DateTime.ParseExact(orderDto.CompletionDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                TotalAmount = orderDto.TotalAmount,
                CustomerId = Guid.Parse(orderDto.CustomerId),
                EmployeeId = Guid.Parse(orderDto.EmployeeId)
            };

            await repository.AddAsync(order);
            await repository.SaveChangesAsync();

            return "Order added Successfully!";
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
