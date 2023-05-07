using Loja.Inspiracao.RabbitMQ.Request;

namespace Loja.Inspiracao.RabbitMQ.Publisher
{
    public interface IMQPublisher
    {
        Task BasicPublishAsync(MQRequest message, CancellationToken cancellationToken = default);
    }
}
