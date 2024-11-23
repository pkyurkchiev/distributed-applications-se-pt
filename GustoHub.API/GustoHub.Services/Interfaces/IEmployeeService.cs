namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;

    public interface IEmployeeService
    {
        Task<string> AddAsync(Employee employee);
        Task<bool> ExistsByIdAsync(Guid employeeId);
        Task<Employee> GetByIdAsync(Guid employeeId);
        Task<Employee> GetByNameAsync(string employeeName);
        Task<IEnumerable<Employee>> AllAsync();
        Task<string> Remove(Guid employeeId);
    }
}
