namespace Loja.Inspiracao.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
