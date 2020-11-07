using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.ShakespeareAPIAdapter.Dto;
using TrueLayer.Connectivity.Challange.Utils;

namespace TrueLayer.Connectivity.Challange.ShakespeareAPIAdapter
{
    public class TranslatorClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUri = new Uri("https://api.funtranslations.com/translate/shakespeare.json/");

        public TranslatorClient(HttpClient httpClient) =>
            _httpClient = httpClient;

        public async Task<Result<string>> GetTranslationAsync(string text)
        {
            var response = await _httpClient.GetAsync($"{_baseUri}?text={WebUtility.UrlEncode(text.Sanitize())}/");
            if (!response.IsSuccessStatusCode)
            {
                return Result<string>.Error(response.ReasonPhrase);
            }
            string translateData = await response.Content.ReadAsStringAsync();
            Translation translation = Deserialize(translateData);
            return Result<string>.Success(translation.Contents.Translated);
        }

        private static Translation Deserialize(string translateData)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            return JsonSerializer.Deserialize<Translation>(translateData, options);
        }
    }
}