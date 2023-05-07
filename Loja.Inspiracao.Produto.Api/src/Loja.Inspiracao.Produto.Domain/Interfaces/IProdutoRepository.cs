using Loja.Inspiracao.Core.Data;
using LojaInspiracao = Loja.Inspiracao.Produto.Domain.Entities;

namespace Loja.Inspiracao.Produto.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<LojaInspiracao.Produto>
    {
        Task<IEnumerable<LojaInspiracao.Produto>> ObterTodosProdutos();
        Task<LojaInspiracao.Produto?>? ObterPorProdutoId(Guid id);
        LojaInspiracao.Produto AdicionarProduto(LojaInspiracao.Produto produto);
        LojaInspiracao.Produto AlterarProduto(LojaInspiracao.Produto produto);
    }
}
