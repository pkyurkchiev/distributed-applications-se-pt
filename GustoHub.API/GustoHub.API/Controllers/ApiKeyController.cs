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

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateApiKey([FromBody] POSTApiKey request)
        {
            var apiKey = await apiKeyService.CreateApiKeyAsync(request.Owner);
            return Ok(new { apiKey });
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeApiKey([FromBody] PUTRevokeApiKey request)
        {
            await apiKeyService.RevokeApiKeyAsync(request.ApiKey);
            return Ok(new { message = "API Key revoked successfully." });
        }
    }
}
