using Loja.Inspiracao.RabbitMQ.Consume;
#nullable disable

namespace Loja.Inspiracao.RabbitMQ.Consume
{
    public static class MQActiveConsumers
    {
        private static IDictionary<string, MQConsumerStatus> _queues;

        public static void SetStatus(string queue, MQConsumerStatus status)
        {
            _queues ??= new Dictionary<string, MQConsumerStatus>();
            _queues[queue] = status;
        }

        public static IEnumerable<KeyValuePair<string, MQConsumerStatus>> GetAll() => _queues?.ToList();
    }
}
