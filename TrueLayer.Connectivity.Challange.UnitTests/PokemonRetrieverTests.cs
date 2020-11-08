using Moq;
using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.Core;
using TrueLayer.Connectivity.Challange.Core.Utils;
using Xunit;

namespace TrueLayer.Connectivity.Challange.UnitTests
{
    public class PokemonRetrieverTests
    {
        [Fact]
        public async Task ShouldReturnPokemonTranslatedDescription()
        {
            var pokemonName = "ditto";
            var pokeApiDescription = "It can freely recombine its own cellular structure to\ntransform into other life-forms.";
            var translatedDescription = "'t can freely recombine its own cellular structure to transform into other life-forms.";
            var expectedDescription = "'t can freely recombine its own cellular structure to transform into other life-forms.";
            var pokeAPIClient = new Mock<IPokeAPIClient>();
            pokeAPIClient.Setup(x => x.GetPokemonDescriptionAsync(It.IsAny<string>())).ReturnsAsync(Result<string>.Success(pokeApiDescription));
            var translatorClient = new Mock<ITranslatorClient>();
            translatorClient.Setup(x => x.GetTranslationAsync(It.IsAny<string>())).ReturnsAsync(Result<string>.Success(translatedDescription));
            var controller = new PokemonRetriever(pokeAPIClient.Object, translatorClient.Object);

            var description = await controller.GetDescriptionAsync(pokemonName);

            Assert.True(description.IsSuccess);
            Assert.Equal(expectedDescription, description.Value);
        }
    }
}
