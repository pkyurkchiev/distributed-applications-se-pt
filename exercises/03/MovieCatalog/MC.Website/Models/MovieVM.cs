using MC.Website.MovieReference;
using System;
using System.ComponentModel.DataAnnotations;

namespace MC.Website.Models
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
            GenreId = movieDto.GenreId;
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Display(Name = "Release date")]
        public DateTime? ReleaseDate { get; set; }
        [Display(Name = "Release country")]
        public string ReleaseCountry { get; set; }

        public int GenreId { get; set; }        
    }
}