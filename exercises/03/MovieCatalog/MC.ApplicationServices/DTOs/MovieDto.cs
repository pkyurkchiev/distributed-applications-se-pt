using MC.Data.Entities;
using System;

namespace MC.ApplicationServices.DTOs
{
    public class MovieDto : BaseDto
    {
        public MovieDto() { }
        public MovieDto(Movie movie)
            : base(movie.Id, movie.IsActive)
        {
            Title = movie.Title;
            ReleaseDate = movie.ReleaseDate;
            ReleaseCountry = movie.ReleaseCountry;            

            GenreId = movie.GenreId.Value;
            DirectorId = movie.DirectorId.Value;
        }

        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseCountry { get; set; }

        public int GenreId { get; set; }
        public int DirectorId { get; set; }
    }
}
