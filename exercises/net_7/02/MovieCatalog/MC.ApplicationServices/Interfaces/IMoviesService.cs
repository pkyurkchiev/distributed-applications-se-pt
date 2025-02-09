using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;

namespace MC.ApplicationServices.Interfaces
{
    /// <summary>
    /// Movie service.
    /// </summary>
    public interface IMoviesService
    {
        /// <summary>
        /// Get list with movies.
        /// </summary>
        /// <param name="request">Get movie request object.</param>
        /// <returns>Return filter list with movies.</returns>
        Task<GetMovieResponse> GetMovieAsync(GetMovieRequest request);

        /// <summary>
        /// Get movie by title.
        /// </summary>
        /// <param name="request">Get title by request object.</param>
        /// <returns>Return single movie by title.</returns>
        Task<GetByTitleResponse> GetByTitleAsync(GetByTitleRequest request);

        /// <summary>
        /// Create movie.
        /// </summary>
        /// <param name="request">Create movie request object.</param>
        /// <returns>Return 200 ok.</returns>
        Task<CreateMovieResponse> SaveAsync(CreateMovieRequest request);
    }
}
