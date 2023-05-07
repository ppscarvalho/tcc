using Loja.Inspiracao.RabbitMQ.Interfaces;

namespace Loja.Inspiracao.RabbitMQ.Interfaces
{
    public interface IMQConnectionFactory
    {
        IMQConnection CreateConnection();
        IMQConnection WaitAvailableConnection();
        Task<IMQConnection> WaitAvailableConnectionAsync();
    }
}
