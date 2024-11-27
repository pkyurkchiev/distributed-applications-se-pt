namespace GustoHub.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class POSTOrderDto
    {
        [Required(ErrorMessage = "Please enter a valid date! Example: 18/02/2024 23:30")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy HH:mm")]
        public string OrderDate { get; set; } = null!;

        [DisplayFormat(DataFormatString = "dd/MM/yyyy HH:mm")]
        public string? CompletionDate { get; set; }

        [Required]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Minimum TotalAmount value cannot be less then zero.")]
        public decimal TotalAmount { get; set; }

        public string CustomerId { get; set; } = null!;

        public string EmployeeId { get; set; } = null!;
    }
}
