using MC.Website.GenreReference;

namespace MC.Website.ViewModels
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