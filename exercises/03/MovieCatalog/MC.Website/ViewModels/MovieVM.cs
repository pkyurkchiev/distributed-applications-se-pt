using MC.Website.MovieReference;
using System;
using System.ComponentModel.DataAnnotations;

namespace MC.Website.ViewModels
{
    public class MovieVM
    {
        public MovieVM() { }
        public MovieVM(MovieDto movieDto)
        {
            Id = movieDto.Id;
            Title = movieDto.Title;
            ReleaseDate = movieDto.ReleaseDate;
            ReleaseCountry = movieDto.ReleaseCountry;
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseCountry { get; set; }
    }
}