namespace GustoHub.Data.Configurations
{
    using GustoHub.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderDishEntityConfiguration : IEntityTypeConfiguration<OrderDish>
    {
        public void Configure(EntityTypeBuilder<OrderDish> builder)
        {
            builder.HasKey(orderDish => new {orderDish.OrderId, orderDish.DishId });
        }
    }
}
