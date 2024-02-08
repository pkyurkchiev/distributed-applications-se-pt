using MC.ApplicationServices.Interfaces;
using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;
using MC.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MC.ApplicationServices.Implementations
{
    /// <inheritdoc/>
    public class MoviesService : IMoviesService
    {
        public MoviesDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesService"/> class.
        /// </summary>
        /// <param name="context">Movie database context.</param>
        public MoviesService(MoviesDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<GetByTitleResponse> GetByTitleAsync(GetByTitleRequest request)
        {
            GetByTitleResponse response = new();

            var movie = await _context.Movies.SingleOrDefaultAsync(x => x.Title == request.Title);
            response.Movie = new()
            {
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate
            };

            return response;
        }
    }
}
