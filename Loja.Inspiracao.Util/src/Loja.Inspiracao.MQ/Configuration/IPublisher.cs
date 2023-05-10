using MassTransit;
using System;
using Loja.Inspiracao.MQ.Events;

namespace Loja.Inspiracao.MQ.Configuration
{
	public interface IPublisher : IEvent
	{
		public string Queue { get; }
		public Action<IRabbitMqBusFactoryConfigurator> Config { get; }
	}
}
