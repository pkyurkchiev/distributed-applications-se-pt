using MC.Data.Entities;
using System;

namespace MC.ApplicationServices.DTOs
{
    public class MovieDto : BaseDto
    {
        public MovieDto() { }
        public MovieDto(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            ReleaseDate = movie.ReleaseDate;
            ReleaseCountry = movie.ReleaseCountry;
            IsActive = movie.IsActive;
        }

        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseCountry { get; set; }    
    }
}
