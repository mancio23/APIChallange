using TrueLayer.Connectivity.Challange.ShakespeareAPIAdapter;
using Xunit;

namespace TrueLayer.Connectivity.Challange.UnitTests
{
    public class TranslatorExtensionsTests
    {
        [Fact]
        public void ShouldSanitizeText()
        {
            var text = "You gave Mr. Tim a hearty meal\n, but unfortunately\fwhat he ate made him die.";
            var expected = "You gave Mr. Tim a hearty meal , but unfortunately what he ate made him die.";

            var sanitizedText = text.Sanitize();

            Assert.Equal(expected, sanitizedText);
        }

        [Fact]
        public void ShouldRemoveLastSlash()
        {
            var text = "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die./";
            var expected = "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.";

            var processedText = text.RemoveLastSlash();

            Assert.Equal(expected, processedText);
        }

    }
}
