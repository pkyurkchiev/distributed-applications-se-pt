using System.ComponentModel.DataAnnotations;

namespace GustoHub.Data.ViewModels.POST
{
    public class POSTUserDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
