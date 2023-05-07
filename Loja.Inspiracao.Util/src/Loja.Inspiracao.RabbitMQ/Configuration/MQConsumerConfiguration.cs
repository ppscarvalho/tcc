using Loja.Inspiracao.RabbitMQ.Configuration;
using Loja.Inspiracao.RabbitMQ.Enums;

namespace Loja.Inspiracao.RabbitMQ.Configuration
{
    public class MQConsumerConfiguration
    {
        public string Queue { get; init; }
        public MQType? MQType { get; set; }
        public bool? QueueDurable { get; set; }
        public bool? QueueExclusive { get; set; }
        public bool? QueueAutoDelete { get; set; }
        public bool? QueueAutoAck { get; set; }
        public int? MessageTtl { get; set; }
        public MQExchangeConfiguration Exchange { get; set; }

        public MQConsumerConfiguration(
            string queue,
            MQType? mqType = null,
            bool? queueDurable = null,
            bool? queueAutoAck = null,
            bool? queueExclusive = null,
            bool? queueAutoDelete = null,
            int? messageTtl = null,
            MQExchangeConfiguration exchange = null)
        {
            Queue = queue;
            MQType = mqType;
            QueueDurable = queueDurable;
            QueueExclusive = queueExclusive;
            QueueAutoDelete = queueAutoDelete;
            QueueAutoAck = queueAutoAck;
            MessageTtl = messageTtl;
            Exchange = exchange;
        }

        public class MQExchangeConfiguration
        {
            public MQExchangeType? Type { get; set; }
            public string Name { get; set; }
            public string Destination { get; set; }
            public string Source { get; set; }
            public bool? Durable { get; set; }
            public bool? AutoDelete { get; set; }
            public Dictionary<string, object> Arguments { get; set; }
        }
    }
}
