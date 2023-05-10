using Loja.Inspiracao.Util.ApiClient;
using Loja.Inspiracao.Util.Extensions;
using Loja.Inspiracao.Util.Options;
using Microsoft.Extensions.Options;
using WebApp.Models;

namespace WebApp.Integracao.Application.Http.Categoria
{
    public class CategoriaApiClient : ApiClientBase, ICategoriaApiClient
    {
        private readonly APIsOptions _apisOptions;

        public CategoriaApiClient(HttpClient httpClient, IOptions<APIsOptions> options) : base(httpClient)
        {
            _apisOptions = options.Value;
        }

        public async Task<IEnumerable<CategoriaViewModel>> ListaCategoria()
        {
            var result = await Get($"{_apisOptions.BaseUrlCategoria}/api/Categoria/lista-categorias");
            return result.DeserializeObject<IEnumerable<CategoriaViewModel>>();
        }
    }
}
