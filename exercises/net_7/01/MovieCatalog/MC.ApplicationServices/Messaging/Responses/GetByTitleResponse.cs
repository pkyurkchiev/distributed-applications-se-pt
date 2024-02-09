namespace MC.ApplicationServices.Messaging.Responses
{
    public class GetByTitleResponse : ServiceResponseBase
    {
        public MovieViewModel Movie { get; set; }
    }
}
