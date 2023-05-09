#nullable disable

using Loja.Inspiracao.Produto.Domain.Entities;
using Loja.Inspiracao.Produto.Domain.Interfaces;
using Loja.Inspiracao.Produto.Infra.Data.Context;
using Loja.Inspiracao.Resources.Data;
using Microsoft.EntityFrameworkCore;

namespace Loja.Inspiracao.Produto.Infra.Data.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ProdutoDbContext _context;
        private bool disposedValue;

        public CategoriaRepository(ProdutoDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Categoria>> ObterTodasCategorias()
        {
            return await _context.Categoria.AsNoTracking().Include(c => c.Produto).ToListAsync();
        }

        public async Task<Categoria> ObterCategoriaPorId(Guid id)
        {
            return await _context.Categoria.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Categoria> AdicionarCategoria(Categoria category)
        {
            return (await _context.AddAsync(category)).Entity;
        }

        public async Task<Categoria> AlterarCategoria(Categoria categoria)
        {
            await Task.CompletedTask;
            _context.Entry(categoria).State = EntityState.Modified;
            return categoria;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
