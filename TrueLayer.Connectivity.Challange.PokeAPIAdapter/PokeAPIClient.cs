using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.Core;
using TrueLayer.Connectivity.Challange.Core.Utils;
using TrueLayer.Connectivity.Challange.PokeAPIAdapter.Dto;

namespace TrueLayer.Connectivity.Challange.PokeAPIAdapter
{
    public class PokeAPIClient : IPokeAPIClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUri = new Uri("https://pokeapi.co/api/v2/pokemon-species/");

        public PokeAPIClient(HttpClient httpClient) =>
            _httpClient = httpClient;

        public async Task<Result<string>> GetPokemonDescriptionAsync(string name)
        {
            var response = await _httpClient.GetAsync($"{_baseUri}{name}/");
            if (!response.IsSuccessStatusCode)
            {
                return Result<string>.Error(response.ReasonPhrase);
            }
            string data = await response.Content.ReadAsStringAsync();
            Pokemon pokemon = Deserialize(data);
            return Result<string>.Success(RetriveEngDescription(pokemon));
        }

        private static string RetriveEngDescription(Pokemon pokemon) =>
             pokemon.FlavorTextEntries.First(x => x.Language.Name == "en").FlavorText;

        private static Pokemon Deserialize(string data) =>
            JsonSerializer.Deserialize<Pokemon>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
    }
}
