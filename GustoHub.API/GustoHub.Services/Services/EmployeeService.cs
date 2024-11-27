namespace GustoHub.Services.Services
{
    using System;
    using GustoHub.Data.Common;
    using GustoHub.Data.Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using GustoHub.Data.ViewModels;
    using System.Globalization;

    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository repository;

        public EmployeeService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<string> AddAsync(POSTEmployeeDto employeeDto)
        {
            Employee employee = new Employee()
            {
                Name = employeeDto.Name,
                Title = employeeDto.Title,
                HireDate = DateTime.ParseExact(employeeDto.HireDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                IsActive = true,
            };

            await repository.AddAsync(employee);
            await repository.SaveChangesAsync();

            return "Employee added Successfully!";
        }

        public async Task<IEnumerable<Employee>> AllAsync()
        {
            return await repository.AllAsync<Employee>();
        }

        public async Task<bool> ExistsByIdAsync(Guid employeeId)
        {
            return await repository.AllAsReadOnly<Employee>().AnyAsync(e => e.Id == employeeId);
        }

        public async Task<Employee> GetByIdAsync(Guid employeeId)
        {
            return await repository.AllAsReadOnly<Employee>().FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<Employee> GetByNameAsync(string employeeName)
        {
            return await repository.AllAsReadOnly<Employee>().FirstOrDefaultAsync(e => e.Name == employeeName);
        }

        public async Task<string> Remove(Guid employeeId)
        {
            if (await ExistsByIdAsync(employeeId))
            {
                await repository.RemoveAsync<Employee>(employeeId);
                await repository.SaveChangesAsync();
                return "Employee removed successfully!";
            }
            return "Employee doesn't exists!";
        }
    }
}
