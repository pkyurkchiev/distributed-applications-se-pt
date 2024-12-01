namespace GustoHub.Services.Services
{
    using System;
    using GustoHub.Data.Common;
    using GustoHub.Data.Models;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.GET;
    using GustoHub.Data.ViewModels.PUT;

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

        public async Task<IEnumerable<GETEmployeeDto>> AllAsync()
        {
            List<GETEmployeeDto> employeeDtos = await repository.AllAsReadOnly<Employee>()
                .Select(e => new GETEmployeeDto()
                {
                    Id = e.Id.ToString(),
                    Name = e.Name,
                    Title = e.Title,
                    HireDate = e.HireDate.ToShortDateString(),
                    IsActive = true,
                })
                .ToListAsync();

            return employeeDtos;
        }

        public async Task<bool> ExistsByIdAsync(Guid employeeId)
        {
            return await repository.AllAsReadOnly<Employee>().AnyAsync(e => e.Id == employeeId);
        }

        public async Task<GETEmployeeDto> GetByIdAsync(Guid employeeId)
        {
            Employee employee = await repository.AllAsReadOnly<Employee>().FirstOrDefaultAsync(e => e.Id == employeeId);

            GETEmployeeDto employeeDto = new GETEmployeeDto()
            {
                Id = employee.Id.ToString(),
                Name = employee.Name,
                Title = employee.Title,
                HireDate = employee.HireDate.ToShortDateString(),
                IsActive = true,
            };

            return employeeDto;
        }

        public async Task<GETEmployeeDto> GetByNameAsync(string employeeName)
        {
            Employee employee = await repository.AllAsReadOnly<Employee>().FirstOrDefaultAsync(e => e.Name == employeeName);

            GETEmployeeDto employeeDto = new GETEmployeeDto()
            {
                Id = employee.Id.ToString(),
                Name = employee.Name,
                Title = employee.Title,
                HireDate = employee.HireDate.ToShortDateString(),
                IsActive = true,
            };

            return employeeDto;
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

        public async Task<string> UpdateAsync(PUTEmployeeDto employeeDto, string employeeId)
        {
            Employee? employee = await repository.GetByIdAsync<Employee>(Guid.Parse(employeeId));

            employee.Name = employeeDto.Name;
            employee.Title = employeeDto.Title;
            employee.HireDate = employee.HireDate;

            await repository.SaveChangesAsync();

            return "Employee updated Successfully!";
        }
    }
}
