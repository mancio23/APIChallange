using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.PokeAPIAdapter.Dto;
using TrueLayer.Connectivity.Challange.Utils;

namespace TrueLayer.Connectivity.Challange.PokeAPIAdapter
{
    public class PokeAPIClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUri = new Uri("https://pokeapi.co/api/v2/pokemon-species/");

        public PokeAPIClient(HttpClient httpClient) =>
            _httpClient = httpClient;

        public async Task<Result<string>> GetPokemonDescriptionAsync(string name)
        {
            var response = await _httpClient.GetAsync($"{_baseUri}{name}/");
            if (!response.IsSuccessStatusCode) return Result<string>.Error(response.ReasonPhrase);
            string data = await response.Content.ReadAsStringAsync();
            Pokemon pokemon = Deserialize(data);
            return Result<string>.Success(RetriveEngDescription(pokemon));
        }

        private static string RetriveEngDescription(Pokemon pokemon) =>
             pokemon.Flavor_text_entries.First(x => x.Language.Name == "en").Flavor_text;

        private static Pokemon Deserialize(string data)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true,
                WriteIndented = true,
            };
            return JsonSerializer.Deserialize<Pokemon>(data, options);
        }
    }
}
