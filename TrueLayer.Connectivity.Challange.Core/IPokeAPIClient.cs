using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.Core.Utils;

namespace TrueLayer.Connectivity.Challange.Core
{
    public interface IPokeAPIClient
    {
        Task<Result<string>> GetPokemonDescriptionAsync(string name);
    }
}