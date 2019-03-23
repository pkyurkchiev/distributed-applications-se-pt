using MC.Website.GenreReference;

namespace MC.Website.Models
{
    public class GenreVM
    {
        public GenreVM() { }
        public GenreVM(GenreDto genreDto)
        {
            Id = genreDto.Id;
            Name = genreDto.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}