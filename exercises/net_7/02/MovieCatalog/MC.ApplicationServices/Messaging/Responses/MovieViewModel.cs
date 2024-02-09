namespace MC.ApplicationServices.Messaging.Responses
{
    public class MovieViewModel
    {
        required public string Title { get; set;}
        public string? Description { get; set;}
        public DateTime? ReleaseDate { get; set;}
    }
}
