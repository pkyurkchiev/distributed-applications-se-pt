using GustoHub.Data.Models;
using GustoHub.Data.ViewModels.POST;
using GustoHub.Data.ViewModels.PUT;
using System.Net;

namespace GustoUIConsole.Services
{
    public class DishService : ApiServiceBase
    {
        public async Task GetAllDishes()
        {
            var response = await _httpClient.GetAsync("/api/Dish/all");
            await HandleResponse(response);
        }

        public async Task GetDishByName(string name)
        {
            var response = await _httpClient.GetAsync($"/api/Dish/{WebUtility.UrlEncode(name)}");
            await HandleResponse(response);
        }

        public async Task CreateDish(POSTDishDto dish)
        {
            var content = CreateJsonContent(dish);
            var response = await _httpClient.PostAsync("/api/Dish", content);
            await HandleResponse(response);
        }

        public async Task UpdateDish(int id, PUTDishDto dish)
        {
            var content = CreateJsonContent(dish);
            var response = await _httpClient.PutAsync($"/api/Dish/{id}", content);
            await HandleResponse(response);
        }

        public async Task DeleteDish(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Dish/{id}");
            await HandleResponse(response);
        }
    }
}
