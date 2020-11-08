using System.Text.RegularExpressions;

namespace TrueLayer.Connectivity.Challange.ShakespeareAPIAdapter
{
    public static class TranslatorExtensions
    {
        public static string Sanitize(this string text) =>
            Regex.Replace(text, @"\r\n?|\n|\f", " ");

        public static string RemoveLastSlash(this string text) =>
            text.EndsWith("/") ? text.Remove(text.Length - 1, 1) : text;
    }
}
