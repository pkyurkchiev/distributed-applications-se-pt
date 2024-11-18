namespace GustoHub.Services.Services
{
    using System;
    using GustoHub.Data.Common;
    using GustoHub.Data.Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CustomerService : ICustomerService
    {
        private readonly IRepository repository;

        public CustomerService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<string> AddAsync(Customer customer)
        {
            if (!await ExistsByIdAsync(customer.Id))
            {
                await repository.AddAsync(customer);
                await repository.SaveChangesAsync();

                return "Customer added Successfully!";
            }

            return "Customer already exists!";
        }

        public async Task<IEnumerable<Customer>> AllAsync()
        {
            return await repository.AllAsync<Customer>();
        }

        public async Task<bool> ExistsByIdAsync(Guid customerId)
        {
            return await repository.AllAsReadOnly<Customer>().AnyAsync(c => c.Id == customerId);
        }

        public async Task<Customer> GetByIdAsync(Guid customerId)
        {
            return await repository.AllAsReadOnly<Customer>().FirstOrDefaultAsync(e => e.Id == customerId);
        }

        public async Task<Customer> GetByNameAsync(string customerName)
        {
            return await repository.AllAsReadOnly<Customer>().FirstOrDefaultAsync(e => e.Name == customerName);
        }

        public async Task<string> Remove(Guid customerId)
        {
            if (await ExistsByIdAsync(customerId))
            {
                await repository.RemoveAsync<Customer>(customerId);
                await repository.SaveChangesAsync();
                return "Customer removed successfully!";
            }
            return "Customer doesn't exists!";
        }
    }
}
