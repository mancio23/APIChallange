using System.Text.Json.Serialization;

namespace TrueLayer.Connectivity.Challange.PokeAPIAdapter.Dto
{
    public class FlavorTextEntry
    {
        [JsonPropertyName("Flavor_text")]
        public string FlavorText { get; set; }
        public Language Language { get; set; }
        public Version Version { get; set; }
    }
}
