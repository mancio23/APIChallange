using RichardSzalay.MockHttp;
using System;
using System.IO;
using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.Core;
using TrueLayer.Connectivity.Challange.PokeAPIAdapter;
using TrueLayer.Connectivity.Challange.ShakespeareAPIAdapter;
using Xunit;

namespace TrueLayer.Connectivity.Challange.UnitTests
{
    public class PokemonRetrieverTests
    {
        [Fact]
        public async Task ShouldReturnPokemonTranslatedDescription()
        {
            var baseUriPoke = new Uri("https://pokeapi.co/api/v2/pokemon-species/");
            var baseUriTranslation = new Uri("https://api.funtranslations.com/translate/shakespeare.json/");
            var pokemonName = "ditto";
            var expectedDescription = "'t can freely recombine its own cellular structure to transform into other life-forms.";
            var jsonPoke = ReadEmbeddedResource($"{pokemonName}-pokemon-species.json");
            var jsonTranslation = ReadEmbeddedResource($"{pokemonName}-translation.json");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When($"{baseUriPoke}{pokemonName}/").Respond("application/json", jsonPoke);
            mockHttp.When($"{baseUriTranslation}*").Respond("application/json", jsonTranslation);
            var pokeAPIClient = new PokeAPIClient(mockHttp.ToHttpClient());
            var translatorClient = new TranslatorClient(mockHttp.ToHttpClient());
            var controller = new PokemonRetriever(pokeAPIClient, translatorClient);

            var description = await controller.GetDescriptionAsync(pokemonName);

            Assert.Equal(expectedDescription, description.Value);
        }

        private static string ReadEmbeddedResource(string resourceName)
        {
            var assembly = typeof(PokemonRetrieverTests).Assembly;
            using var stream = assembly.GetManifestResourceStream($"TrueLayer.Connectivity.Challange.UnitTests.Json.{resourceName}");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
