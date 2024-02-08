using System;

namespace MC.ApplicationServices.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Country { get; set; }
        public string ScreenWriter { get; set; }
        public GenreDto Genre { get; set; }
        public DirectorDto Director { get; set; }
    }
}
