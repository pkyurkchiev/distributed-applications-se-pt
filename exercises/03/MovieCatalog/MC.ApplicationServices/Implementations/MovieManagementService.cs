using MC.ApplicationServices.DTOs;
using MC.Data.Contexts;
using MC.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MC.ApplicationServices.Implementations
{
    public class MovieManagementService
    {
        #region Properties
        private readonly MovieCatalogDbContext _context = new MovieCatalogDbContext();
        #endregion

        #region public Methods
        public List<MovieDto> Get()
        {
            List<MovieDto> movieDtos = new List<MovieDto>();

            foreach (var movie in _context.Movies.Where(x => x.IsActive == true).ToList())
            {
                movieDtos.Add(new MovieDto(movie));
            }
            return movieDtos;
        }

        public List<MovieDto> GetByTitle(string title)
        {
            List<Movie> movies = _context.Movies.Where(x => x.Title.Contains(title) && x.IsActive == true).ToList();

            return movies.Select(x => new MovieDto(x)).ToList();
        }

        public int Save(MovieDto movieDto)
        {
            Movie movie = new Movie
            {
                Title = movieDto.Title,
                ReleaseDate = movieDto.ReleaseDate,
                ReleaseCountry = movieDto.ReleaseCountry,
                GenreId = movieDto.GenreId,
                DirectorId = movieDto.DirectorId,
                IsActive = movieDto.IsActive
            };

            try
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();

                return movie.Id;
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
                Movie movie = _context.Movies.Find(id);
                if (movie == null)
                    return -1;

                _context.Movies.Remove(movie);
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
