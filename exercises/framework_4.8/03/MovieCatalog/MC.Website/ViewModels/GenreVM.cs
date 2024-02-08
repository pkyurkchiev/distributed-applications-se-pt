namespace MC.Website.ViewModels
{
    public class GenreVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GenreVM() { }
        public GenreVM(int id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}