using MC.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MC.Data.Contexts
{
    /// <summary>
    /// Movie database context.
    /// </summary>
    public class MoviesDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets movie dbset collection.
        /// </summary>
        public DbSet<Movie> Movies { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesDbContext"/> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options) { }
    }
}
