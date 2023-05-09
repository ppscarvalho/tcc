using Loja.Inspiracao.Resources.DomainObject;
using System;

namespace Loja.Inspiracao.Resources.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
