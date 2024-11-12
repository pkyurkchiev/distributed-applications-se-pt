namespace GustoHub.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public ICollection<OrderDish> OrderDishes { get; set; } = new List<OrderDish>();
    }
}
