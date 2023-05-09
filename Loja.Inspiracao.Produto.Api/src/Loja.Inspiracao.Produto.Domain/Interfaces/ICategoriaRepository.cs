using Loja.Inspiracao.Produto.Domain.Entities;
using Loja.Inspiracao.Resources.Data;

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
