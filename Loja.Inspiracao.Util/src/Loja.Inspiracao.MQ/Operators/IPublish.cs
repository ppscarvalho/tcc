using System.Threading.Tasks;

namespace Loja.Inspiracao.MQ.Operators
{
    public interface IPublish
    {
        Task DoPublish<T>(T obj);
        Task DoSend<T>(T obj);
        Task<TResponse> DoRPC<TRequest, TResponse>(TRequest obj, int timeout = 120) where TRequest : class where TResponse : class;
    }
}
