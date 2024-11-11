namespace GustoHub.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty!;

        [MaxLength(100)]
        public string Title { get; set; } = string.Empty!;

        [Required]
        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
