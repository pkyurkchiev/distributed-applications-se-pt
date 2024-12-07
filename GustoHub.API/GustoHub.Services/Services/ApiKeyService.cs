namespace GustoHub.Services.Services
{
    using GustoHub.Data;
    using GustoHub.Data.Models;
    using GustoHub.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography;

    public class ApiKeyService : IApiKeyService
    {
        private readonly GustoHubDbContext dbContext;

        public ApiKeyService(GustoHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateApiKeyAsync(string owner)
        {
            var newApiKey = GenerateApiKey();
            var apiKey = new ApiKey
            {
                Key = newApiKey,
                Owner = owner,
                CreatedAt = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(30),
                IsActive = true
            };

            dbContext.ApiKeys.Add(apiKey);
            await dbContext.SaveChangesAsync();

            return newApiKey;
        }

        public async Task<bool> IsValidApiKeyAsync(string apiKey)
        {
            var key = await dbContext.ApiKeys
                .Where(k => k.Key == apiKey && k.IsActive &&
                            (k.ExpirationDate == null || k.ExpirationDate > DateTime.UtcNow))
                .FirstOrDefaultAsync();

            return key != null;
        }

        public async Task RevokeApiKeyAsync(string apiKey)
        {
            var key = await dbContext.ApiKeys.FirstOrDefaultAsync(k => k.Key == apiKey);
            if (key != null)
            {
                key.IsActive = false;
                await dbContext.SaveChangesAsync();
            }
        }
        private string GenerateApiKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var apiKeyBytes = new byte[32];
                rng.GetBytes(apiKeyBytes);
                return Convert.ToBase64String(apiKeyBytes);
            }
        }
    }
}
