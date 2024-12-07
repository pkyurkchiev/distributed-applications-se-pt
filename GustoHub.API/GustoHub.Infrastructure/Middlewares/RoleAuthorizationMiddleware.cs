using GustoHub.Data.Models;
using GustoHub.Infrastructure.Attributes;
using Microsoft.AspNetCore.Http;

namespace GustoHub.Infrastructure.Middlewares
{
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate next;

        public RoleAuthorizationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.Items["User"] as User;

            if (user == null)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\": \"User not found.\"}");
                return;
            }

            var endpoint = context.GetEndpoint();
            var roleAttribute = endpoint?.Metadata.OfType<AuthorizeRoleAttribute>().FirstOrDefault();

            if (roleAttribute != null && user.Role != roleAttribute.Role)
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
