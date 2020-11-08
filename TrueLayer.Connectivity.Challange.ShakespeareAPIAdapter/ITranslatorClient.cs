using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.Utils;

namespace TrueLayer.Connectivity.Challange.ShakespeareAPIAdapter
{
    public interface ITranslatorClient
    {
        Task<Result<string>> GetTranslationAsync(string text);
    }
}