namespace MC.ApplicationServices.Messaging.Requests
{
    public class MovieModel
    {
        required public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? ReleaseDate { get; set; }
    }
}
