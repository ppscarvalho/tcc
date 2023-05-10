using Loja.Inspiracao.Util.ApiClient;
using WebApp.Models;

namespace WebApp.Integracao.Application.Http.Categoria
{
    public interface ICategoriaApiClient : IApiClientBase
    {
        Task<IEnumerable<CategoriaViewModel>> ListaCategoria();
    }
}
