using System.Threading.Tasks;

namespace Loja.Inspiracao.Resources.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
