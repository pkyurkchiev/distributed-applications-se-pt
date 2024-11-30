namespace GustoHub.Data.ViewModels.PUT
{
    using System.ComponentModel.DataAnnotations;

    public class PUTCustomerDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be bigger then 100 symbols.")]
        public string Name { get; set; } = string.Empty!;

        [Required]
        [MaxLength(150, ErrorMessage = "Email cannot be bigger then 150 symbols.")]
        public string Email { get; set; } = string.Empty!;

        [MaxLength(15)]
        public string Phone { get; set; } = string.Empty!;
    }
}
