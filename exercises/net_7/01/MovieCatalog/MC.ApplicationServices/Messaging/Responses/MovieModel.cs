namespace MC.ApplicationServices.Messaging.Responses
{
    public class MovieModel
    {
        required public string Title { get; set;}
        public string? Description { get; set;}
        public DateTime? ReleaseDate { get; set;}
    }
}
