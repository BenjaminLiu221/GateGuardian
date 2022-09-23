using System.Text.Json.Serialization;

namespace GateGuardianWeb.Models.YelpGraphQL
{
    public class SearchResponseType
    {
        [JsonPropertyName("search")]
        public Search Search { get; set; }
    }
}
