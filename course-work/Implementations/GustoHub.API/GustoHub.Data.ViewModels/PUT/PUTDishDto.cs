namespace GustoHub.Data.ViewModels.PUT
{
    using System.ComponentModel.DataAnnotations;

    public class PUTDishDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty!;

        [Required]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Minimum Price value cannot be less then zero.")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
