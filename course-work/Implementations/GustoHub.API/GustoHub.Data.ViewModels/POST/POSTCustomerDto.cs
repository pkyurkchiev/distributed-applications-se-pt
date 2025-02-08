namespace GustoHub.Data.ViewModels.POST
{
    using System.ComponentModel.DataAnnotations;

    public class POSTCustomerDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be bigger then 100 symbols.")]
        public string Name { get; set; } = string.Empty!;

        [Required]
        [MaxLength(150, ErrorMessage = "Email cannot be bigger then 150 symbols.")]
        public string Email { get; set; } = string.Empty!;

        [MaxLength(15)]
        [RegularExpression(@"^(?:\+359|00359|0)\s?\d{1,3}\s?\d{6}$",
        ErrorMessage = "Please enter a valid Bulgarian phone number in the format" +
            " +359XXXXXXXXX," +
            " 00359XXXXXXXXX, or 0XXXXXXXXX.")]
        public string Phone { get; set; } = string.Empty!;
    }
}
