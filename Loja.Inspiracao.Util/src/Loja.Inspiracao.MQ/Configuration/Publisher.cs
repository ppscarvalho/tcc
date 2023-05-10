using MassTransit;
using RabbitMQ.Client;
using System;
using Loja.Inspiracao.MQ.Configuration;
using Loja.Inspiracao.MQ.Events;

namespace Loja.Inspiracao.MQ.Configuration
{
	public class Publisher<T> : IPublisher where T : class, IEvent
	{
		public Publisher(string queue, bool durable = true, bool autoDelete = false)
		{
			ConfigurePublish(queue, durable, autoDelete);
		}

		public Publisher(string queue, string exchangeType, string exchangeName, string RoutingKey, bool durable = true, bool autoDelete = false)
		{
			ConfigurePublish(queue, durable, autoDelete, exchangeType, exchangeName, RoutingKey);
		}

		private void ConfigurePublish(string queue, bool durable, bool autoDelete, string exchangeType = ExchangeType.Fanout, string exchangeName = "", string RoutingKey = "")
		{
			Queue = queue;

			if (string.IsNullOrEmpty(exchangeName) is false)
			{
				Config += c => c.Message<T>(x => x.SetEntityName(exchangeName));
			}

			Config += c => c.Publish<T>(x =>
			{
				x.Durable = durable;
				x.AutoDelete = autoDelete;
				x.ExchangeType = exchangeType;
			});

			if (string.IsNullOrEmpty(RoutingKey) is false)
			{
				Config += c => c.Send<T>(x =>
				{
					x.UseRoutingKeyFormatter(context => RoutingKey);
					x.UseCorrelationId(context => Guid.NewGuid());

				});
			}
		}

		public string Queue { get; private set; }
		public Action<IRabbitMqBusFactoryConfigurator> Config { get; private set; }
		public string Host { get; set; }
	}
}
