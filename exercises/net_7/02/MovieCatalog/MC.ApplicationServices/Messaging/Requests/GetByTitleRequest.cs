namespace MC.ApplicationServices.Messaging.Requests
{
    public class GetByTitleRequest : ServiceRequestBase
    {
        public string Title { get; set; }

        public GetByTitleRequest(string title)
        {
            Title = title;
        }
    }
}
