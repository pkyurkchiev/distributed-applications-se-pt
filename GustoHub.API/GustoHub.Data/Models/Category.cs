namespace GustoHub.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty!;

        public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
    }
}
