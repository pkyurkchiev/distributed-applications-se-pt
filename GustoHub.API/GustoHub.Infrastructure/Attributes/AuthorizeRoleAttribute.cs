using GustoHub.Data.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GustoHub.Infrastructure.Attributes
{
    public class AuthorizeRoleAttribute : Attribute
    {
        public string Role;

        public AuthorizeRoleAttribute(string role)
        {
            this.Role = role;
        }
    }
}
