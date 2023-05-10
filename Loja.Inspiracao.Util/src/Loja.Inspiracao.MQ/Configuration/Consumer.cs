using System;

namespace Loja.Inspiracao.MQ.Configuration
{
	public class Consumer
	{
		public Consumer(string queue, Type typeConsumer, bool quorumQueue, bool durable = true, bool autoDelete = false)
		{
			Queue = queue;
			TypeConsumer = typeConsumer;
			QuorumQueue = quorumQueue;
			Durable = durable;
			AutoDelete = autoDelete;
		}
		public Consumer(string queue, Type typeConsumer, string routingKey, string exchangeType, string exchangeName, bool quorumQueue, bool durable = true, bool autoDelete = false)
		{
			Queue = queue;
			TypeConsumer = typeConsumer;
			QuorumQueue = quorumQueue;
			Durable = durable;
			AutoDelete = autoDelete;
			RoutingKey = routingKey;
			ExchangeType = exchangeType;
			ExchangeName = exchangeName;
		}

		public string Queue { get; private set; }
		public Type TypeConsumer { get; private set; }
		public bool QuorumQueue { get; private set; }
		public bool AutoDelete { get; private set; } = false;
		public bool Durable { get; private set; } = true;
		public string RoutingKey { get; set; }
		public string ExchangeType { get; set; }
		public string ExchangeName { get; set; }
	}
}
