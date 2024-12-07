namespace GustoHub.Data.ViewModels.PUT
{
    using System.ComponentModel.DataAnnotations;

    public class PUTRevokeApiKey
    {
        [Required]
        public string ApiKey { get; set; } = null!;
    }
}
