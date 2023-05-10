#nullable disable

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Inspiracao.Util.ApiClient
{
    public interface IApiClientBase
    {
        Task<string> Post(string urlServico, object obj, Dictionary<string, string> headers = null);
        Task<string> Put(string urlServico, object obj, Dictionary<string, string> headers = null);
        Task<string> Get(string urlServico, object obj = null, Dictionary<string, string> headers = null);
        Task<string> Delete(string urlServico, object obj = null, Dictionary<string, string> headers = null);
    }
}
