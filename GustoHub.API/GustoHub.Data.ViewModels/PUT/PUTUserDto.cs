using System.ComponentModel.DataAnnotations;

namespace GustoHub.Data.ViewModels.PUT
{
    public class PUTUserDto
    {
        [Required]
        public string Role { get; set; } = null!;
    }
}
