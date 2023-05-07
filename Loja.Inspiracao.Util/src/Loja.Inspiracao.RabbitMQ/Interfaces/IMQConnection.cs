using RabbitMQ.Client;

namespace Loja.Inspiracao.RabbitMQ.Interfaces
{
    public interface IMQConnection : IDisposable
    {
        IAutorecoveringConnection Connection { get; }
        IModel Model { get; }
        bool Retry { get; }
        int RetryCount { get; }
    }
}
