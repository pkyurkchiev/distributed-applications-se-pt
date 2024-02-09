using System.Text.Json.Serialization;

namespace MC.ApplicationServices.Messaging.Responses
{
    public class GetByTitleResponse : ServiceResponseBase
    {
        [JsonIgnore]
        public MovieViewModel? Movie { get; set; }
    }
}
