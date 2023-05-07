using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using Loja.Inspiracao.RabbitMQ.Interfaces;
using Loja.Inspiracao.RabbitMQ.Publisher;
using Loja.Inspiracao.RabbitMQ.Request;

namespace Loja.Inspiracao.RabbitMQ.Publisher
{
    public class MQPublisher : IMQPublisher
    {
        private readonly IMQConnectionFactory _connectionFactory;

        public MQPublisher(IMQConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Post the message to RabbitMQ
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        public async Task BasicPublishAsync(MQRequest message, CancellationToken cancellationToken = default)
        {
            using var rabbitConnection = await _connectionFactory.WaitAvailableConnectionAsync();
            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(message.RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) => throw ex);

            using var channel = rabbitConnection.Model;

            policy.Execute(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(message.Exchange))
                        channel.QueueDeclare(message.RoutingKey, true, false, false, new Dictionary<string, object> { { "x-queue-type", "quorum" } });
                }
                finally
                {
                    channel.BasicPublish(exchange: message.Exchange, routingKey: message.RoutingKey, basicProperties: message.Properties, body: message.Body);
                }
            });
        }
    }
}
