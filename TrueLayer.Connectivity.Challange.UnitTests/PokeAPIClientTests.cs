using RichardSzalay.MockHttp;
using System;
using System.IO;
using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.PokeAPIAdapter;
using Xunit;

namespace TrueLayer.Connectivity.Challange.UnitTests
{
    public class PokeAPIClientTests
    {
        private readonly Uri _baseUri = new Uri("https://pokeapi.co/api/v2/pokemon-species/");

        [Theory]
        [InlineData("ditto", "It can freely recombine its own cellular structure to\ntransform into other life-forms.")]
        [InlineData("charizard", "Spits fire that\nis hot enough to\nmelt boulders.\fKnown to cause\nforest fires\nunintentionally.")]
        [InlineData("pikachu", "When several of\nthese POKÈMON\ngather, their\felectricity could\nbuild and cause\nlightning storms.")]
        public async Task ShouldRetrievePokemonDescription(string pokemonName, string expectedDescription)
        {
            var json = ReadEmbeddedResource($"{pokemonName}-pokemon-species.json");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When($"{_baseUri}{pokemonName}/").Respond("application/json", json);
            var sut = new PokeAPIClient(mockHttp.ToHttpClient());

            var response = await sut.GetPokemonDescriptionAsync(pokemonName);

            Assert.Equal(expectedDescription, response);
        }

        private static string ReadEmbeddedResource(string resourceName)
        {
            var assembly = typeof(PokeAPIClientTests).Assembly;
            using var stream = assembly.GetManifestResourceStream($"TrueLayer.Connectivity.Challange.UnitTests.Json.{resourceName}");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
