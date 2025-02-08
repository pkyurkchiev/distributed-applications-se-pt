namespace GustoHub.Data.Models
{
    public class OrderDish
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;

        public int DishId { get; set; }
        public virtual Dish Dish { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
