using GustoHub.Data.Models;
using GustoHub.Data.ViewModels.POST;
using GustoHub.Data.ViewModels.PUT;

namespace GustoUIConsole.Services
{
    public class UserService : ApiServiceBase
    {
        public async Task GetUserById(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/User/{Uri.EscapeDataString(userId)}");
            await HandleResponse(response);
        }

        public async Task CreateUser(POSTUserDto user)
        {
            var content = CreateJsonContent(user);
            var response = await _httpClient.PostAsync("/api/User", content);
            await HandleResponse(response);
        }

        public async Task UpdateUser(string id, PUTUserDto user)
        {
            var content = CreateJsonContent(user);
            var response = await _httpClient.PutAsync($"/api/User/{id}", content);
            await HandleResponse(response);
        }

        public async Task VerifyUser(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/User/verify?userId={Uri.EscapeDataString(userId)}");
            await HandleResponse(response);
        }
    }
}
