using WebApp.Models;
using WebApp.Utils.ApiClient;

namespace WebApp.Integracao.Categoria
{
    public interface ICategoriaApiClient : IApiClientBase
    {
        Task<IEnumerable<CategoriaViewModel>> ListaCategoria();
    }
}
