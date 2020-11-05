using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrueLayer.Connectivity.Challange.PokeAPIAdapter.Dto
{
    public class Pokemon
    {
        [JsonPropertyName("Flavor_text_entries")]
        public IEnumerable<FlavorTextEntry> FlavorTextEntries { get; set; }
    }
}

