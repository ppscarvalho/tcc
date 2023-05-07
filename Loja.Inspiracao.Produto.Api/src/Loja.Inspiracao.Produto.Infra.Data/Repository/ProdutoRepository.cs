#nullable disable

using Loja.Inspiracao.Core.Data;
using Loja.Inspiracao.Produto.Domain.Interfaces;
using Loja.Inspiracao.Produto.Infra.Data.Context;
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
            return await _context.Produto.AsNoTracking().ToListAsync();
        }

        public async Task<LojaInspiracao.Produto> ObterPorProdutoId(Guid id)
        {
            return await _context.Produto.FirstOrDefaultAsync(e => e.Id == id);
        }

        public LojaInspiracao.Produto AdicionarProduto(LojaInspiracao.Produto produto)
        {
            return _context.Produto.Add(produto).Entity;
        }

        public LojaInspiracao.Produto AlterarProduto(LojaInspiracao.Produto product)
        {
            return _context.Produto.Update(product).Entity;
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
