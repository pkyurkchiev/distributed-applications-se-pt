namespace GustoHub.Data.ViewModels.PUT
{
    using System.ComponentModel.DataAnnotations;

    public class PUTEmployeeDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be bigger then 100 symbols.")]
        public string Name { get; set; } = string.Empty!;

        [Required]
        [MaxLength(100, ErrorMessage = "Title cannot be bigger then 100 symbols.")]
        public string Title { get; set; } = string.Empty!;

        [Required(ErrorMessage = "Please enter a valid date! Example: 18/02/2024 23:30")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy HH:mm")]
        public string HireDate { get; set; } = null!;
    }
}
