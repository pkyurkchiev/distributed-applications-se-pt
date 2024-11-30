namespace GustoHub.Services.Services
{
    using System;
    using GustoHub.Data.Common;
    using GustoHub.Data.Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.GET;

    public class CustomerService : ICustomerService
    {
        private readonly IRepository repository;

        public CustomerService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<string> AddAsync(POSTCustomerDto customerDto)
        {
            Customer customer = new Customer()
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
            };

            await repository.AddAsync(customer);
            await repository.SaveChangesAsync();

            return "Customer added Successfully!";
        }

        public async Task<IEnumerable<GETCustomerDto>> AllAsync()
        {
            List<GETCustomerDto> customerDtos = await repository.AllAsReadOnly<Customer>()
                .Select(c => new GETCustomerDto()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone
                })
                .ToListAsync();
                                  
            return customerDtos;
        }

        public async Task<bool> ExistsByIdAsync(Guid customerId)
        {
            return await repository.AllAsReadOnly<Customer>().AnyAsync(c => c.Id == customerId);
        }

        public async Task<GETCustomerDto> GetByIdAsync(Guid customerId)
        {
            Customer customer = await repository.AllAsReadOnly<Customer>().FirstOrDefaultAsync(e => e.Id == customerId);

            GETCustomerDto customerDto = new GETCustomerDto()
            {
                Id = customer.Id.ToString(),
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email
            };

            return customerDto;
        }

        public async Task<GETCustomerDto> GetByNameAsync(string customerName)
        {
            Customer customer = await repository.AllAsReadOnly<Customer>().FirstOrDefaultAsync(e => e.Name == customerName);

            GETCustomerDto customerDto = new GETCustomerDto()
            {
                Id = customer.Id.ToString(),
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email
            };

            return customerDto;
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
