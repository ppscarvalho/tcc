using Loja.Inspiracao.Produto.Application.ViewModels;

namespace Loja.Inspiracao.Produto.Application.Queries.Categoria
{
    public interface ICategoriaQueries
    {
        Task<IEnumerable<CategoriaViewModel>> ObterTodasCategorias();
        Task<CategoriaViewModel> ObterCategoriaPodId(Guid id);
    }
}
