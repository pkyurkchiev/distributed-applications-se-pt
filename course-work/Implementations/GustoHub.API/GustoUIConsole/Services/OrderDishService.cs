using GustoHub.Data.Models;
using GustoHub.Data.ViewModels.POST;

namespace GustoUIConsole.Services
{
    public class OrderDishService : ApiServiceBase
    {
        public async Task GetAllOrderDishes()
        {
            var response = await _httpClient.GetAsync("/api/OrderDish/all");
            await HandleResponse(response);
        }

        public async Task GetDishesByOrder(int orderId)
        {
            var response = await _httpClient.GetAsync($"/api/OrderDish/allDishesBy/{orderId}");
            await HandleResponse(response);
        }

        public async Task GetOrderDish(int orderId, int dishId)
        {
            var response = await _httpClient.GetAsync($"/api/OrderDish/{orderId}/{dishId}");
            await HandleResponse(response);
        }

        public async Task CreateOrderDish(POSTOrderDishDto orderDish)
        {
            var content = CreateJsonContent(orderDish);
            var response = await _httpClient.PostAsync("/api/OrderDish", content);
            await HandleResponse(response);
        }
    }
}
