using Loja.Inspiracao.MQ.Models;
using WebApp.Models;

namespace WebApp.Integracao.Application.Interfaces
{
    public interface ICategoriaAppService
    {
        Task<CategoriaViewModel> ObterCategoriaPorId(Guid id);
        Task<IEnumerable<CategoriaViewModel>> ObterTodasCategorias();
        Task<ResponseOut> SalvarCategoria(CategoriaViewModel categoriaViewModel);
        Task<ResponseOut> AlterarCategoria(CategoriaViewModel categoriaViewModel);
    }
}
