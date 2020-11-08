using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.IO;
using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.API.Controllers;
using TrueLayer.Connectivity.Challange.API.Model;
using TrueLayer.Connectivity.Challange.PokeAPIAdapter;
using TrueLayer.Connectivity.Challange.ShakespeareAPIAdapter;
using Xunit;

namespace TrueLayer.Connectivity.Challange.UnitTests
{
    public class PokemonControllerTests
    {
        [Fact]
        public async Task ShouldReturnPokemonTranslatedDescription()
        {
            var baseUriPoke = new Uri("https://pokeapi.co/api/v2/pokemon-species/");
            var baseUriTranslation = new Uri("https://api.funtranslations.com/translate/shakespeare.json/");
            var pokemonName = "ditto";
            var expectedDescription = "'t can freely recombine its own cellular structure to transform into other life-forms.";
            var log = new Mock<ILogger<string>>();
            var jsonPoke = ReadEmbeddedResource($"{pokemonName}-pokemon-species.json");
            var jsonTranslation = ReadEmbeddedResource($"{pokemonName}-translation.json");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When($"{baseUriPoke}{pokemonName}/").Respond("application/json", jsonPoke);
            mockHttp.When($"{baseUriTranslation}*").Respond("application/json", jsonTranslation);
            var pokeAPIClient = new PokeAPIClient(mockHttp.ToHttpClient());
            var translatorClient = new TranslatorClient(mockHttp.ToHttpClient());
            var controller = new PokemonController(pokeAPIClient, translatorClient, log.Object);

            var response = await controller.GetAsync(pokemonName);
            var result = (ObjectResult)response.Result;
            var pokemonResult = (PokemonResult)result.Value;

            Assert.Equal(pokemonName, pokemonResult.Name);
            Assert.Equal(expectedDescription, pokemonResult.Description);
        }

        private static string ReadEmbeddedResource(string resourceName)
        {
            var assembly = typeof(PokemonControllerTests).Assembly;
            using var stream = assembly.GetManifestResourceStream($"TrueLayer.Connectivity.Challange.UnitTests.Json.{resourceName}");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
