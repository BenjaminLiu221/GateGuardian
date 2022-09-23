using System.Text.Json.Serialization;

namespace GateGuardianWeb.Models.YelpGraphQL
{
    public class Search
    {
        [JsonPropertyName("business")]
        public List<Business> business { get; set; }
    }
}
