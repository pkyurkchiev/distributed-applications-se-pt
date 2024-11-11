namespace GustoHub.Data
{
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

        //TODO:
        //Add DbSet...

        /*
         * The OnModelCreating method is overridden to configure the model and relationships using Fluent API.
         * Calling base.OnModelCreating(modelBuilder) ensures that any configurations
         * set up in the base class are applied.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
