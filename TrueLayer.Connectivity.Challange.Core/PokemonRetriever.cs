using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.Core.Utils;

namespace TrueLayer.Connectivity.Challange.Core
{
    public class PokemonRetriever : IPokemonRetriever
    {
        private readonly IPokeAPIClient _pokemonClient;
        private readonly ITranslatorClient _translationClient;

        public PokemonRetriever(IPokeAPIClient pokemonClient, ITranslatorClient translationClient)
        {
            _pokemonClient = pokemonClient;
            _translationClient = translationClient;
        }

        public async Task<Result<string>> GetDescriptionAsync(string name)
        {
            var description = await _pokemonClient.GetPokemonDescriptionAsync(name);
            if (!description.IsSuccess)
            {
                return description;
            }

            return await _translationClient.GetTranslationAsync(description.Value);
        }
    }
}
