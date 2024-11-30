namespace GustoHub.Services.Services
{
    using GustoHub.Data.Common;
    using GustoHub.Data.Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.GET;
   

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

        public async Task<IEnumerable<GETOrdersDto>> AllAsync()
        {
            List<GETOrdersDto> orderDtos = await repository.AllAsReadOnly<Order>()
                .Select(o => new GETOrdersDto()
                {
                    Id = o.Id.ToString(),
                    EmployeeId = o.EmployeeId.ToString(),
                    CustomerId = o.CustomerId.ToString(),
                    CompletionDate = o.CompletionDate.HasValue
                     ? o.OrderDate.ToString("f")
                     : null,
                    OrderDate = o.OrderDate.ToShortDateString(),
                    TotalAmount = o.TotalAmount.ToString("F2")
                })
                .ToListAsync();

            return orderDtos;
        }

        public async Task<bool> ExistsByIdAsync(int orderId)
        {
            return await repository.AllAsReadOnly<Order>().AnyAsync(o => o.Id == orderId);
        }

        public async Task<GETOrdersDto> GetByIdAsync(int orderId)
        {
            Order order = await repository.AllAsReadOnly<Order>().FirstOrDefaultAsync(o => o.Id == orderId);

            GETOrdersDto orderDto = new GETOrdersDto()
            {
                Id = order.Id.ToString(),
                EmployeeId = order.EmployeeId.ToString(),
                CustomerId = order.CustomerId.ToString(),
                CompletionDate = order.CompletionDate.HasValue
                     ? order.OrderDate.ToString("f")
                     : null,
                OrderDate = order.OrderDate.ToShortDateString(),
                TotalAmount = order.TotalAmount.ToString("F2")
            };

            return orderDto;
        }

        public async Task<GETOrdersDto> GetByDateAsync(DateTime date)
        {
            Order order = await repository.AllAsReadOnly<Order>().FirstOrDefaultAsync(o => o.OrderDate == date);

            GETOrdersDto orderDto = new GETOrdersDto()
            {
                Id = order.Id.ToString(),
                EmployeeId = order.EmployeeId.ToString(),
                CustomerId = order.CustomerId.ToString(),
                CompletionDate = order.CompletionDate.HasValue
                     ? order.OrderDate.ToString("f")
                     : null,
                OrderDate = order.OrderDate.ToShortDateString(),
                TotalAmount = order.TotalAmount.ToString("F2")
            };

            return orderDto;
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
