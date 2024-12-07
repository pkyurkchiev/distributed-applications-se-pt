namespace GustoHub.Data.ViewModels.POST
{
    using System.ComponentModel.DataAnnotations;

    public class POSTApiKey
    {
        [Required]
        public string Owner { get; set; } = null!;
    }
}
