using MC.Data.Entities;

namespace MC.ApplicationServices.DTOs
{
    public class GenreDto : BaseDto
    {
        public GenreDto() { }
        public GenreDto(Genre genre)
            : base(genre.Id, genre.IsActive)
        {
            Name = genre.Name;
        }

        public string Name { get; set; }
    }
}
