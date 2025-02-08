namespace GustoHub.Infrastructure.Middlewares
{
    using GustoHub.Data.Models;
    using GustoHub.Infrastructure.Attributes;
    using Microsoft.AspNetCore.Http;

    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate next;

        public RoleAuthorizationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            var roleAttribute = endpoint?.Metadata
                .OfType<AuthorizeRoleAttribute>()
                .FirstOrDefault();

            if (roleAttribute == null)
            {
                await next(context);
                return;
            }

            var user = context.Items["User"] as User;

            if (user == null || user.Role != roleAttribute.Role)
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\": \"Forbidden: Insufficient permissions.\"}");
                return;
            }

            await next(context);
        }
    }
}
