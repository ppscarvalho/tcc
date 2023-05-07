using Loja.Inspiracao.Core.Data;
using Loja.Inspiracao.Core.DomainObject;

namespace Loja.Inspiracao.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
