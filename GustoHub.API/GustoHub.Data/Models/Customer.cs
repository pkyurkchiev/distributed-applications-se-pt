namespace GustoHub.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty!;

        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty!;

        [MaxLength(15)]
        [RegularExpression("^(?:\\+359|00359|0)\\s?\\d{1,3}\\s?\\d{6}$")]
        public string Phone { get; set; } = string.Empty!;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
