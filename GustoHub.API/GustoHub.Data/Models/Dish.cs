namespace GustoHub.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Dish
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty!;

        [Required]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public ICollection<OrderDish> OrderDishes { get; set; } = new List<OrderDish>();
    }
}
