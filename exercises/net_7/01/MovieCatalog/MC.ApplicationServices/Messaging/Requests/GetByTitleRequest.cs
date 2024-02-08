namespace MC.ApplicationServices.Messaging.Requests
{
    public class GetByTitleRequest : RequestServiceBase
    {
        public string Title { get; set; }

        public GetByTitleRequest(string title)
        {
            Title = title;
        }
    }
}
