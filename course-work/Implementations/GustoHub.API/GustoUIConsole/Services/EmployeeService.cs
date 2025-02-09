using GustoHub.Data.ViewModels.GET;
using GustoHub.Data.ViewModels.POST;
using GustoHub.Data.ViewModels.PUT;

namespace GustoUIConsole.Services
{
    public class EmployeeService : ApiServiceBase
    {
        public async Task GetActiveEmployees()
        {
            var response = await _httpClient.GetAsync("/api/Employee/all-active");
            await HandleResponse(response);
        }

        public async Task GetInactiveEmployees()
        {
            var response = await _httpClient.GetAsync("/api/Employee/all-deactivated");
            await HandleResponse(response);
        }

        public async Task GetEmployeeByName(string name)
        {
            var response = await _httpClient.GetAsync($"/api/Employee/{Uri.EscapeDataString(name)}");
            await HandleResponse(response);
        }

        public async Task CreateEmployee(POSTEmployeeDto employee, string userId)
        {
            var query = !string.IsNullOrEmpty(userId) ? $"?userId={Uri.EscapeDataString(userId)}" : "";
            var content = CreateJsonContent(employee);
            var response = await _httpClient.PostAsync($"/api/Employee{query}", content);
            await HandleResponse(response);
        }

        public async Task UpdateEmployee(string id, PUTEmployeeDto employee)
        {
            var content = CreateJsonContent(employee);
            var response = await _httpClient.PutAsync($"/api/Employee/{id}", content);
            await HandleResponse(response);
        }

        public async Task ActivateEmployee(string id)
        {
            var response = await _httpClient.PutAsync($"/api/Employee/activate/{id}", null);
            await HandleResponse(response);
        }

        public async Task DeactivateEmployee(string id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Employee/deactivate/{id}");
            await HandleResponse(response);
        }
    }
}
