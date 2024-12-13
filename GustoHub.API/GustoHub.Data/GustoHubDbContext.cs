namespace GustoHub.Data
{
    using System.Reflection;
    using GustoHub.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class GustoHubDbContext : DbContext
    {
        /*
         * The constructor takes a parameter of type DbContextOptions<GustoHubDbContext>,
         * which configures the context with specific options (like connection strings and database providers).
         * The base constructor is called with these options to initialize the context properly.
         */
        public GustoHubDbContext(DbContextOptions<GustoHubDbContext> options)
            :base(options)
        {           
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDish> OrderDishes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<ApiKey> ApiKeys { get; set; } = null!;

        /*
         * The OnModelCreating method is overridden to configure the model and relationships using Fluent API.
         * Calling base.OnModelCreating(modelBuilder) ensures that any configurations
         * set up in the base class are applied.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(GustoHubDbContext))
                ?? Assembly.GetExecutingAssembly();

            modelBuilder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
