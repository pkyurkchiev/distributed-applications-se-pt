namespace GustoHub.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Dish
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty!;

        [Required]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Minimum Price value cannot be less then zero.")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

        public ICollection<OrderDish> OrderDishes { get; set; } = new List<OrderDish>();
    }
}
