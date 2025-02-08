namespace GustoHub.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class APIKeyRequiredAttribute : Attribute
    {
    }
}
