using Loja.Inspiracao.Core.Data;
using Loja.Inspiracao.Produto.Domain.Entities;

namespace Loja.Inspiracao.Produto.Domain.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> ObterTodasCategorias();
        Task<Categoria> ObterCategoriaPorId(Guid id);
        Task<Categoria> AdicionarCategoria(Categoria categoria);
        Task<Categoria> AlterarCategoria(Categoria categoria);
    }
}
