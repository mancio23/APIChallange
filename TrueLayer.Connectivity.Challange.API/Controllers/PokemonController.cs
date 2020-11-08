using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.API.Model;
using TrueLayer.Connectivity.Challange.Core;

namespace TrueLayer.Connectivity.Challange.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRetriever _pokemonRetriever;
        private readonly ILogger<string> _logger;

        public PokemonController(IPokemonRetriever pokemonRetriever, ILogger<string> logger)
        {
            _pokemonRetriever = pokemonRetriever;
            _logger = logger;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<PokemonResult>> GetAsync(string name)
        {
            var description = await _pokemonRetriever.GetDescriptionAsync(name);
            if (!description.IsSuccess)
            {
                var message = $"PokemonRetrieverError pokemonName = {name}, error = {description}";
                _logger.LogError(message);
                return NotFound(message);
            }

            return Ok(new PokemonResult() { Name = name, Description = description.Value });
        }
    }
}
