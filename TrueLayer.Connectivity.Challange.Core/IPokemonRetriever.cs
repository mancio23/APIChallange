using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.Core.Utils;

namespace TrueLayer.Connectivity.Challange.Core
{
    public interface IPokemonRetriever
    {
        Task<Result<string>> GetDescriptionAsync(string name);
    }
}