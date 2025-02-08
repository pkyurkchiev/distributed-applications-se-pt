namespace GustoHub.Services.Interfaces
{
    using GustoHub.Data.Models;

    public interface IApiKeyService
    {
        Task<string> CreateApiKeyAsync(string owner);
        Task<bool> IsValidApiKeyAsync(string apiKey);
        Task RevokeApiKeyAsync(string apiKey);
        Task<User?> GetUserByApiKeyAsync(string apiKey);
    }
}
