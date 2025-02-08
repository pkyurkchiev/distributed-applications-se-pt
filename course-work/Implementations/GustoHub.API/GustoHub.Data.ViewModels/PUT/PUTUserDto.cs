using System.ComponentModel.DataAnnotations;

namespace GustoHub.Data.ViewModels.PUT
{
    public class PUTUserDto
    {
        [Required]
        public bool IsVerified { get; set; }

        public string? Role { get; set; }
    }
}
