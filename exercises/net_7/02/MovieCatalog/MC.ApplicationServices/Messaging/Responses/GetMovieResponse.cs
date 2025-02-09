namespace MC.ApplicationServices.Messaging.Responses
{
    public class GetMovieResponse : ServiceResponseBase
    {
        public List<MovieViewModel> Movies { get; set; }
    }
}
