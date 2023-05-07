using RabbitMQ.Client;
using Loja.Inspiracao.RabbitMQ.Enums;

namespace Loja.Inspiracao.RabbitMQ.Extension
{
    public static class MQExtensions
    {
        public static string XQueueType(this MQType? queueType) =>
            queueType switch
            {
                MQType.Quorum => "quorum",
                MQType.Classic => "classic",
                _ => "quorum"
            };

        public static string XExchangeType(this MQExchangeType? exchangeType) =>
            exchangeType switch
            {
                MQExchangeType.Direct => ExchangeType.Direct,
                MQExchangeType.Fanout => ExchangeType.Fanout,
                MQExchangeType.Headers => ExchangeType.Headers,
                MQExchangeType.Topic => ExchangeType.Topic,
                _ => ExchangeType.Direct
            };
    }
}
