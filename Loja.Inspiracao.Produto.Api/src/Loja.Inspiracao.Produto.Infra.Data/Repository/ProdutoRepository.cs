#nullable disable

using Loja.Inspiracao.Produto.Domain.Interfaces;
using Loja.Inspiracao.Produto.Infra.Data.Context;
using Loja.Inspiracao.Resources.Data;
using Microsoft.EntityFrameworkCore;
using LojaInspiracao = Loja.Inspiracao.Produto.Domain.Entities;

namespace Loja.Inspiracao.Produto.Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoDbContext _context;
        private bool disposedValue;

        public ProdutoRepository(ProdutoDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<LojaInspiracao.Produto>> ObterTodosProdutos()
        {
            return await _context.Produto.AsNoTracking().Include(p => p.Categoria).ToListAsync();
        }

        public async Task<LojaInspiracao.Produto> ObterPorProdutoId(Guid id)
        {
            return await _context.Produto.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<LojaInspiracao.Produto> AdicionarProduto(LojaInspiracao.Produto produto)
        {
            return (await _context.AddAsync(produto)).Entity;
        }

        public async Task<LojaInspiracao.Produto> AlterarProduto(LojaInspiracao.Produto product)
        {
            await Task.CompletedTask;
            _context.Entry(product).State = EntityState.Modified;
            return product;
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
