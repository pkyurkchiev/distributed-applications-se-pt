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
        /// Get movie by title.
        /// </summary>
        /// <param name="request">Get title by request object.</param>
        /// <returns>Return single movie by title.</returns>
        Task<GetByTitleResponse> GetByTitleAsync(GetByTitleRequest request);
    }
}
