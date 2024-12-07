using System.Security.Cryptography;

namespace GustoHub.Services.Interfaces
{
    public interface IApiKeyService
    {
        Task<string> CreateApiKeyAsync(string owner);
        Task<bool> IsValidApiKeyAsync(string apiKey);
        Task RevokeApiKeyAsync(string apiKey);
    }
}
