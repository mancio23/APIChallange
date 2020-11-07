using System.Text.RegularExpressions;

namespace TrueLayer.Connectivity.Challange.ShakespeareAPIAdapter
{
    public static class TranslatorExtensions
    {
        public static string Sanitize(this string text) =>
            Regex.Replace(text, @"\r\n?|\n|\f", " ");
    }
}
