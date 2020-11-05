using RichardSzalay.MockHttp;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.ShakespeareAPIAdapter;
using Xunit;

namespace TrueLayer.Connectivity.Challange.UnitTests
{
    public class TranslatorClientTests
    {
        private readonly Uri _baseUri = new Uri("https://api.funtranslations.com/translate/shakespeare.json/");

        [Fact]
        public async Task ShouldRetrieveTranslation()
        {
            var description = "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.";
            var expected = "Thee did giveth mr. Tim a hearty meal,  but unfortunately what he did doth englut did maketh him kicketh the bucket.";
            var json = ReadEmbeddedResource("translation.json");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When($"{_baseUri}*").Respond("application/json", json);
            var sut = new TranslatorClient(mockHttp.ToHttpClient());

            var response = await sut.GetTranslationAsync(description);

            Assert.True(response.IsSuccess);
            Assert.Equal(expected, response.Value);
        }

        [Theory]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.BadRequest)]
        public async Task SuccessFalseIfUnableToTranslate(HttpStatusCode httpStatusCode)
        {
            var description = "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.";
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When($"{_baseUri}*").Respond(httpStatusCode);
            var sut = new TranslatorClient(mockHttp.ToHttpClient());

            var response = await sut.GetTranslationAsync(description);

            Assert.False(response.IsSuccess);
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
