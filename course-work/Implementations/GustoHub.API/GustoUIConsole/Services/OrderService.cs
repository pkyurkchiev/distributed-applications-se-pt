using GustoHub.Data.Models;
using GustoHub.Data.ViewModels.POST;
using GustoHub.Data.ViewModels.PUT;
using System.Net.Http;

namespace GustoUIConsole.Services
{
    public class OrderService : ApiServiceBase
    {
        public async Task GetAllOrders()
        {
            var response = await _httpClient.GetAsync("/api/Order/all");
            await HandleResponse(response);
        }

        public async Task GetOrderByDate(string date)
        {
            var response = await _httpClient.GetAsync($"/api/Order/{Uri.EscapeDataString(date)}");
            await HandleResponse(response);
        }

        public async Task CreateOrder(POSTOrderDto order)
        {
            var content = CreateJsonContent(order);
            var response = await _httpClient.PostAsync("/api/Order", content);
            await HandleResponse(response);
        }

        public async Task UpdateOrder(int id, PUTOrderDto order)
        {
            var content = CreateJsonContent(order);
            var response = await _httpClient.PutAsync($"/api/Order/{id}", content);
            await HandleResponse(response);
        }

        public async Task DeleteOrder(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Order/{id}");
            await HandleResponse(response);
        }
    }
}
