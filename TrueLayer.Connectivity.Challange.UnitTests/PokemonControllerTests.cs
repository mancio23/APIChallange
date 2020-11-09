using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.API.Controllers;
using TrueLayer.Connectivity.Challange.API.Model;
using TrueLayer.Connectivity.Challange.Core;
using TrueLayer.Connectivity.Challange.Core.Utils;
using Xunit;

namespace TrueLayer.Connectivity.Challange.UnitTests
{
    public class PokemonControllerTests
    {
        [Fact]
        public async Task ShouldReturnPokemonTranslatedDescription()
        {
            var pokemonName = "ditto";
            var expectedDescription = "'t can freely recombine its own cellular structure to transform into other life-forms.";
            var log = new Mock<ILogger<string>>();
            var pokemonRetriever = new Mock<IPokemonRetriever>();
            pokemonRetriever.Setup(x => x.GetDescriptionAsync(It.IsAny<string>())).ReturnsAsync(Result<string>.Success(expectedDescription));
            var controller = new PokemonController(pokemonRetriever.Object, log.Object);

            var response = await controller.GetAsync(pokemonName);
            var result = (ObjectResult)response.Result;
            var pokemonResult = (Pokemon)result.Value;

            Assert.Equal(pokemonName, pokemonResult.Name);
            Assert.Equal(expectedDescription, pokemonResult.Description);
        }

        [Fact]
        public async Task ShouldReturnNotFound()
        {
            var pokemonName = "ditto";
            var log = new Mock<ILogger<string>>();
            var mockHttp = new MockHttpMessageHandler();
            var pokemonRetriever = new Mock<IPokemonRetriever>();
            pokemonRetriever.Setup(x => x.GetDescriptionAsync(It.IsAny<string>())).ReturnsAsync(Result<string>.Error("Error"));
            var controller = new PokemonController(pokemonRetriever.Object, log.Object);

            var response = await controller.GetAsync(pokemonName);
            var result = (ObjectResult)response.Result;

            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode.Value);
        }
    }
}
