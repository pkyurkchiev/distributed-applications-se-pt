using MC.ApplicationServices.DTOs;
using MC.Data.Contexts;
using MC.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MC.ApplicationServices.Implementations
{
    public class GenreManagementService
    {
        #region Properties
        private readonly MovieCatalogDbContext _context = new MovieCatalogDbContext();
        #endregion

        #region public Methods
        public List<GenreDto> Get()
        {
            List<GenreDto> genreDtos = new List<GenreDto>();

            foreach (var genre in _context.Genres.ToList())
            {
                genreDtos.Add(new GenreDto(genre));
            }
            return genreDtos;
        }

        public GenreDto GetById(int id)
        {
            return new GenreDto(_context.Genres.Find(id));
        }

        public int Save(GenreDto genreDto)
        {
            Genre genre = new Genre
            {
                Name = genreDto.Name,
                IsActive = genreDto.IsActive
            };

            try
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();

                return genre.Id;
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
                Genre genre = _context.Genres.Find(id);
                if (genre == null)
                    return -1;

                _context.Genres.Remove(genre);
                _context.SaveChanges();

                return id;
            }
            catch (System.Exception)
            {
                return -1;
            }
        }
        #endregion

    }
}
