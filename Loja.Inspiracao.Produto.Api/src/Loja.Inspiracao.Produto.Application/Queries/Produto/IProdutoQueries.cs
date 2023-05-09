using Loja.Inspiracao.Produto.Application.ViewModels;

namespace Loja.Inspiracao.Produto.Application.Queries.Produto
{
    public interface IProdutoQueries
    {
        Task<IEnumerable<ProdutoViewModel>> ObterTodosProdutos();
        Task<ProdutoViewModel> ObterProdutoPorId(Guid id);
    }
}
