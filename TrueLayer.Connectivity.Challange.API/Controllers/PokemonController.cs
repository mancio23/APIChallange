using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.API.Model;
using TrueLayer.Connectivity.Challange.PokeAPIAdapter;
using TrueLayer.Connectivity.Challange.ShakespeareAPIAdapter;

namespace TrueLayer.Connectivity.Challange.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokeAPIClient _pokemonClient;
        private readonly ITranslatorClient _translationClient;
        private readonly ILogger<string> _logger;

        public PokemonController(IPokeAPIClient pokemonClient, ITranslatorClient translationClient, ILogger<string> logger)
        {
            _pokemonClient = pokemonClient;
            _translationClient = translationClient;
            _logger = logger;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<PokemonResult>> GetAsync(string name)
        {
            var description = await _pokemonClient.GetPokemonDescriptionAsync(name);
            if (!description.IsSuccess)
            {
                var message = $"PokemonClientError pokemonName = {name}, error = {description}";
                _logger.LogError(message);
                return NotFound(message);
            }

            var translation = await _translationClient.GetTranslationAsync(description.Value);
            if (!translation.IsSuccess)
            {
                var message = $"TranslationClientError text = {description.Value}, error = {translation}";
                _logger.LogError(message);
                return NotFound(message);
            }

            return Ok(new PokemonResult() { Name = name, Description = translation.Value });
        }
    }
}
