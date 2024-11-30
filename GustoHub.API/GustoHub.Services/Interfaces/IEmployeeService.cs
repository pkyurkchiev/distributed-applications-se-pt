namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;
    using GustoHub.Data.ViewModels.GET;
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;

    public interface IEmployeeService
    {
        Task<string> AddAsync(POSTEmployeeDto employeeDto);
        Task<bool> ExistsByIdAsync(Guid employeeId);
        Task<GETEmployeeDto> GetByIdAsync(Guid employeeId);
        Task<GETEmployeeDto> GetByNameAsync(string employeeName);
        Task<IEnumerable<GETEmployeeDto>> AllAsync();
        Task<string> Remove(Guid employeeId);
        Task<string> UpdateAsync(PUTEmployeeDto employee, string employeeId);
    }
}
