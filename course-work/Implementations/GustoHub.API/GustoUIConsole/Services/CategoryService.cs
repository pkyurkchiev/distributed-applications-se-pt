using GustoHub.Data.ViewModels.POST;
using GustoHub.Data.ViewModels.PUT;
using System.Net;

namespace GustoUIConsole.Services
{
    public class CategoryService : ApiServiceBase
    {
        public async Task GetAllCategories()
        {
            var response = await _httpClient.GetAsync("/api/Categories/all");
            await HandleResponse(response);
        }

        public async Task GetCategoryByName(string name)
        {
            var response = await _httpClient.GetAsync($"/api/Categories/{WebUtility.UrlEncode(name)}");
            await HandleResponse(response);
        }

        public async Task CreateCategory(POSTCategoryDto category)
        {
            var content = CreateJsonContent(category);
            var response = await _httpClient.PostAsync("/api/Categories", content);
            await HandleResponse(response);
        }

        public async Task UpdateCategory(int id, PUTCategoryDto category)
        {
            var content = CreateJsonContent(category);
            var response = await _httpClient.PutAsync($"/api/Categories/{id}", content);
            await HandleResponse(response);
        }

        public async Task DeleteCategory(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Categories/{id}");
            await HandleResponse(response);
        }
    }
}
