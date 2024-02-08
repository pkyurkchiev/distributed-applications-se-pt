using MC.ApplicationServices.DTOs;
using MC.ApplicationServices.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace MC.ApplicationServices
{
    public class GenresManagementService : BaseManagementService
    {
        public IEnumerable<GenreDto> GetAll()
        {
            return _context.Genres.AsNoTracking().AsEnumerable().ToGenreDtos();
        }

        public GenreDto GetById(int id)
        {
            return _context.Genres.Find(id).ToGenreDto();
        }

        public int Save(GenreDto genreDto)
        {
            try
            {
                _context.Genres.Add(genreDto.ToGenreEntity());
                _context.SaveChanges();
                return 1;
            }
            catch (System.Exception)
            {
                return -1;
            }
        }

        public int Delete(int id)
        {
            try
            {
                var genre = _context.Genres.Find(id);
                if (genre == null)
                    return -1;

                _context.Genres.Remove(genre);
                _context.SaveChanges();
                return 1;
            }
            catch (System.Exception)
            {
                return -1;
            }
        }
    }
}
