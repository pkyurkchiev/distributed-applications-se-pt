namespace GustoHub.Infrastructure.Middlewares
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using GustoHub.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using GustoHub.Infrastructure.Attributes;

    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate next;
        private const string APIKEY_HEADER = "X-API-KEY";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            if (context.Request.Path.StartsWithSegments("/api/ApiKey/generate"))
            {
                await next(context);
                return;
            }

            var requiresApiKey = endpoint?.Metadata
                .OfType<APIKeyRequiredAttribute>()
                .Any() ?? false;

            if (!requiresApiKey) 
            {
                await next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(APIKEY_HEADER, out var apiKey))
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\": \"API Key is missing.\"}");
                return;
            }

            var apiKeyService = context
                .RequestServices.GetRequiredService<IApiKeyService>();

            if (!await apiKeyService.IsValidApiKeyAsync(apiKey))
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\": \"Invalid API Key.\"}");
                return;
            }

            await next(context);
        }

    }
}
