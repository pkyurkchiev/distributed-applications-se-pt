namespace GustoHub.Data.ViewModels.POST
{
    using System.ComponentModel.DataAnnotations;

    public class POSTApiKey
    {
        [Required]
        public string UserId { get; set; } = null!;
    }
}
