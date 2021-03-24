using MC.ApplicationServices.DTOs;
using MC.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MC.ApplicationServices.Helpers
{
    public static class ConvertHelper
    {
        #region Movie methods
        public static Movie ToMovieEntity(this MovieDto movieDto)
        {
            return new Movie 
            {
                Id = movieDto.Id,
                Title = movieDto.Title
            };
        }

        public static MovieDto ToMovieDto(this Movie movie)
        {
            return new MovieDto 
            {
                Id = movie.Id,
                Title = movie.Title
            };
        }

        public static IEnumerable<MovieDto> ToMovieDtos(this IEnumerable<Movie> movies)
        {
            return movies.Select(x => x.ToMovieDto());
        }
        #endregion

        #region Genre methods
        public static Genre ToGenreEntity(this GenreDto genreDto)
        {
            return new Genre
            {
                Id = genreDto.Id,
                GenreName = genreDto.Name
            };
        }

        public static GenreDto ToGenreDto(this Genre genre)
        {
            return new GenreDto
            {
                Id = genre.Id,
                Name = genre.GenreName
            };
        }

        public static IEnumerable<GenreDto> ToGenreDtos(this IEnumerable<Genre> genres)
        {
            return genres.Select(x => x.ToGenreDto());
        }
        #endregion

        #region Director methods
        public static Director ToDirectorEntity(this DirectorDto directorDto)
        {
            return new Director
            {
                Id = directorDto.Id,
                FirstName = directorDto.FirstName,
                LastName = directorDto.LastName,
                UserName = directorDto.UserName
            };
        }

        public static DirectorDto ToDirectorDto(this Director director)
        {
            return new DirectorDto
            {
                Id = director.Id,
                FirstName = director.FirstName,
                LastName = director.LastName,
                UserName = director.UserName
            };
        }

        public static IEnumerable<DirectorDto> ToDirectorDtos(this IEnumerable<Director> directors)
        {
            return directors.Select(x => x.ToDirectorDto());
        }
        #endregion
    }
}
