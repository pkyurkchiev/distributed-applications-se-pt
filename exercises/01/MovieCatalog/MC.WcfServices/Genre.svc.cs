using MC.ApplicationServices.DTOs;
using MC.ApplicationServices.Implementations;
using System.Collections.Generic;

namespace MC.WcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Genre" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Genre.svc or Genre.svc.cs at the Solution Explorer and start debugging.
    public class Genre : IGenre
    {
        #region Properties
        private readonly GenreManagementService _service = new GenreManagementService();
        #endregion
        public List<GenreDto> GetGenres()
        {
            return _service.Get();
        }

        public string PostGenre(GenreDto genreDto)
        {
            if (_service.Save(genreDto) == -1)
                return "Genre is not inserted";

            return "Genre is inserted";
        }

        public string DeleteMovie(int id)
        {
            if (_service.Delete(id) == -1)
                return $"Genre with id {id} is not deleted";

            return $"Genre with id {id} deleted";
        }
    }
}
