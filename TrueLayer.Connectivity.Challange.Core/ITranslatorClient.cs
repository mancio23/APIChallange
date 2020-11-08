using System.Threading.Tasks;
using TrueLayer.Connectivity.Challange.Core.Utils;

namespace TrueLayer.Connectivity.Challange.Core
{
    public interface ITranslatorClient
    {
        Task<Result<string>> GetTranslationAsync(string text);
    }
}