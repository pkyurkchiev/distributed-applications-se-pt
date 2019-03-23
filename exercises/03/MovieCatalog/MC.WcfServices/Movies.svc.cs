using MC.ApplicationServices.DTOs;
using MC.ApplicationServices.Implementations;
using System.Collections.Generic;

namespace MC.WcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Movies" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Movies.svc or Movies.svc.cs at the Solution Explorer and start debugging.
    public class Movies : IMovies
    {
        #region Properties
        private readonly MovieManagementService _service = new MovieManagementService();
        #endregion
        public List<MovieDto> GetMovies()
        {
            return _service.Get();
        }
        
        public List<MovieDto> GetMoviesByTitle(string title)
        {
            return _service.GetByTitle(title);
        }

        public string PostMovie(MovieDto movieDto)
        {
            if (_service.Save(movieDto) == -1)
                return "Movie is not inserted";

            return "Movie is inserted";
        }

        public string DeleteMovie(int id)
        {
            if (_service.Delete(id) == -1)
                return $"Movie with id {id} is not deleted";

            return $"Movie with id {id} deleted";
        }

        public string Message()
        {
            return "Wfc is running on localhost:49...";
        }
    }
}
