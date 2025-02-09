using MC.ApplicationServices.Interfaces;
using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;
using MC.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MC.ApplicationServices.Implementations
{
    /// <inheritdoc/>
    public class MoviesService : IMoviesService
    {
        private readonly ILogger<MoviesService> _logger;
        private readonly MoviesDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesService"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="context">Movie database context.</param>
        public MoviesService(ILogger<MoviesService> logger, MoviesDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<GetMovieResponse> GetMovieAsync(GetMovieRequest request)
        {
            GetMovieResponse response = new() { Movies = new() };

            var movies = await _context.Movies.Where(x => x.IsActive == request.IsActive).ToListAsync();
            if (movies is null)
            {
                return response;
            }
            foreach (var movie in movies)
            {
                response.Movies.Add(new()
                {
                    Title = movie.Title,
                    Description = movie.Description,
                    ReleaseDate = movie.ReleaseDate
                });
            }

            return response;
        }

        /// <inheritdoc/>
        public async Task<GetByTitleResponse> GetByTitleAsync(GetByTitleRequest request)
        {
            GetByTitleResponse response = new();

            var movie = await _context.Movies.SingleOrDefaultAsync(x => x.Title == request.Title);
            if (movie is null)
            {
                _logger.LogInformation("Movie is not found with title: {title}", request.Title);
                response.StatusCode = Messaging.BusinessStatusCodeEnum.MissingObject;
                return response;
            }

            response.Movie = new()
            {
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate
            };

            return response;
        }

        /// <inheritdoc/>
        public async Task<CreateMovieResponse> SaveAsync(CreateMovieRequest request)
        {
            CreateMovieResponse response = new();

            try
            {
                await _context.Movies.AddAsync(new()
                {
                    Title = request.Movie.Title,
                    Description = request.Movie.Description,
                    ReleaseDate = request.Movie.ReleaseDate
                });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Movie is not save.");
                response.StatusCode = Messaging.BusinessStatusCodeEnum.InternalServerError;
                return response;
            }


            return response;
        }
    }
}
