using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TrueLayer.Connectivity.Challange.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private ILogger<string> _logger;

        public PokemonController(ILogger<string> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return string.Empty;
        }
    }
}
