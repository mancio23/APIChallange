using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.Utils;

namespace TrueLayer.Connectivity.Challange.PokeAPIAdapter
{
    public interface IPokeAPIClient
    {
        Task<Result<string>> GetPokemonDescriptionAsync(string name);
    }
}