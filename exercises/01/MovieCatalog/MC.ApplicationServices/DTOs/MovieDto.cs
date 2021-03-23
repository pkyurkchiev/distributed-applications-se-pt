using MC.Data.Entities;

namespace MC.ApplicationServices.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public MovieDto() { }
        public MovieDto(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
        }
    }
}
