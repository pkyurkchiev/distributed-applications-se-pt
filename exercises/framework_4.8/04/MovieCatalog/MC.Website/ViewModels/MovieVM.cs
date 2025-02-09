namespace MC.Website.ViewModels
{
    public class MovieVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string Genre { get; set; }
        public string DirectorFullName { get; set; }

        public MovieVM() { }
        public MovieVM(int id, string title, string country, string genre, string directorFullName)
        {
            Id = id;
            Title = title;
            Country = country;
            Genre = genre;
            DirectorFullName = directorFullName;
        }
    }
}