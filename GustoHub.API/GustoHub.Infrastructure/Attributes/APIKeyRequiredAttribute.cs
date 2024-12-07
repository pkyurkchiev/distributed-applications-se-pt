namespace GustoHub.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class APIKeyRequiredAttribute : Attribute
    {
    }
}
