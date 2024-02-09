namespace MC.ApplicationServices.Messaging.Requests
{
    public class GetMovieRequest : ServiceRequestBase
    {
        public bool IsActive { get; set; }

        public GetMovieRequest(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
