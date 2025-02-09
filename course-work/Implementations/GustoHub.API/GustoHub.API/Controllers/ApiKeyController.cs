namespace GustoHub.API.Controllers
{
    using GustoHub.Data.ViewModels.POST;
    using GustoHub.Data.ViewModels.PUT;
    using GustoHub.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/ApiKey")]
    public class ApiKeyController : ControllerBase
    {
        private readonly IApiKeyService apiKeyService;

        public ApiKeyController(IApiKeyService apiKeyService)
        {
            this.apiKeyService = apiKeyService;
        }

        /// <summary>
        /// Generates a new API key for a user.
        /// </summary>
        /// <param name="request">The request containing the User ID.</param>
        /// <returns>The generated API key.</returns>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateApiKey([FromBody] POSTApiKey request)
        {
            var apiKey = await apiKeyService.CreateApiKeyAsync(request.UserId);
            return Ok(new { apiKey });
        }

        /// <summary>
        /// Revokes an existing API key.
        /// </summary>
        /// <param name="request">The request containing the API key to be revoked.</param>
        /// <returns>A success message confirming the revocation.</returns>
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeApiKey([FromBody] PUTRevokeApiKey request)
        {
            await apiKeyService.RevokeApiKeyAsync(request.ApiKey);
            return Ok(new { message = "API Key revoked successfully." });
        }
    }
}
