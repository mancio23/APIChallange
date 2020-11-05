using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.ShakespeareAPIAdapter.Dto;

namespace TrueLayer.Connectivity.Challange.ShakespeareAPIAdapter
{
    public class TranslatorClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUri = new Uri("https://api.funtranslations.com/translate/shakespeare.json/");

        public TranslatorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTranslationAsync(string text)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true,
                WriteIndented = true,
            };

            var translateResponse = await _httpClient.GetAsync($"{_baseUri}?text={WebUtility.UrlEncode(text)}/");
            using HttpContent translateContent = translateResponse.Content;
            string translateData = await translateContent.ReadAsStringAsync();
            var translation = JsonSerializer.Deserialize<Translation>(translateData, options);
            return translation.Contents.Translated;
        }
    }
}