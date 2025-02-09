using GustoHub.Data.ViewModels.POST;
using GustoHub.Data.ViewModels.PUT;
using System.Net;

namespace GustoUIConsole.Services
{
    public class CustomerService : ApiServiceBase
    {
        public async Task GetAllCustomers()
        {
            var response = await _httpClient.GetAsync("/api/Customer/all");
            await HandleResponse(response);
        }

        public async Task GetCustomerByName(string name)
        {
            var response = await _httpClient.GetAsync($"/api/Customer/{WebUtility.UrlEncode(name)}");
            await HandleResponse(response);
        }

        public async Task CreateCustomer(POSTCustomerDto customer)
        {
            var content = CreateJsonContent(customer);
            var response = await _httpClient.PostAsync("/api/Customer", content);
            await HandleResponse(response);
        }

        public async Task UpdateCustomer(string id, PUTCustomerDto customer)
        {
            var content = CreateJsonContent(customer);
            var response = await _httpClient.PutAsync($"/api/Customer/{id}", content);
            await HandleResponse(response);
        }

        public async Task DeleteCustomer(string id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Customer/{id}");
            await HandleResponse(response);
        }
    }
}
