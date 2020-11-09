using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        public async Task<ActionResult<Pokemon>> GetAsync(string name)
        {
            try
            {
                var description = await _pokemonRetriever.GetDescriptionAsync(name);
                if (!description.IsSuccess)
                {
                    var message = $"PokemonControllerInfo pokemonName = {name}, message = {description}";
                    _logger.LogInformation(message);
                    return NotFound(message);
                }

                return Ok(new Pokemon() { Name = name, Description = description.Value });
            }
            catch(Exception ex)
            {
                var message = $"PokemonControllerError pokemonName = {name}, message = {ex.Message}";
                _logger.LogError(message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
