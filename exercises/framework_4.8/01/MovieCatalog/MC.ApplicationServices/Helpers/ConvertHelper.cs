using MC.ApplicationServices.DTOs;
using MC.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MC.ApplicationServices.Helpers
{
    public static class ConvertHelper
    {
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
    }
}
