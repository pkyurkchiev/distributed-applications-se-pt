namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;
    using GustoHub.Data.ViewModels;

    public interface IEmployeeService
    {
        Task<string> AddAsync(POSTEmployeeDto employeeDto);
        Task<bool> ExistsByIdAsync(Guid employeeId);
        Task<Employee> GetByIdAsync(Guid employeeId);
        Task<Employee> GetByNameAsync(string employeeName);
        Task<IEnumerable<Employee>> AllAsync();
        Task<string> Remove(Guid employeeId);
    }
}
