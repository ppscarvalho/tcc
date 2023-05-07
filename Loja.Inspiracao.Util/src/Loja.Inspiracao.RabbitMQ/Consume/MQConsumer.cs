using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.ComponentModel.DataAnnotations;
using Loja.Inspiracao.RabbitMQ.Configuration;
using Loja.Inspiracao.RabbitMQ.Consume;
using Loja.Inspiracao.RabbitMQ.Consume.Interface;
using Loja.Inspiracao.RabbitMQ.Extension;
using Loja.Inspiracao.RabbitMQ.Extensions;
using Loja.Inspiracao.RabbitMQ.Interfaces;

namespace Loja.Inspiracao.RabbitMQ.Consume
{
    public abstract class MQConsumer<T> : BackgroundService where T : class, IMQConsumer, new()
    {
        private readonly IMQConnectionFactory _connectionFactory;
        private readonly ILogger _logger;
        private readonly MQConsumerConfiguration _configuration;
        private const string RetryHeader = "RETRY-COUNT";

        protected MQConsumer(IMQConnectionFactory connectionFactory, ILogger logger, MQConsumerConfiguration configuration)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Method for consuming RabbitMQ messages.
        /// </summary>
        /// <param name="model">Parsed and validated model.</param>
        /// <param name="eventArgs">Message metadata.</param>
        /// <returns>Boolean indicating the success of the operation.</returns>
        protected abstract bool OnConsume(T model, BasicDeliverEventArgs eventArgs);


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            MQActiveConsumers.SetStatus(_configuration.Queue, MQConsumerStatus.WaitingBroker);

            var rabbitConnection = await _connectionFactory.WaitAvailableConnectionAsync();

            MQActiveConsumers.SetStatus(_configuration.Queue, MQConsumerStatus.Ready);
            var consumer = new EventingBasicConsumer(rabbitConnection.Model);

            RegisterConsumer(consumer, rabbitConnection);
            RegisterEvents(rabbitConnection);
            RegisterQueue(consumer, rabbitConnection);
            RegisterExchange(consumer, rabbitConnection);
        }

        private void RegisterConsumer(EventingBasicConsumer consumer, IMQConnection rabbitConnection)
        {
            consumer.Received += (channel, eventArgs) =>
            {
                try
                {
                    MQActiveConsumers.SetStatus(_configuration.Queue, MQConsumerStatus.Running);
                    IEnumerable<ValidationResult> validationResult = Enumerable.Empty<ValidationResult>();

                    // Decodes and validates the object.
                    if (eventArgs.Body.TryDecodeToObject(out T model) && model.Validate(out validationResult))
                    {
                        // Call the handler.
                        if (OnConsume(model, eventArgs))
                        {
                            rabbitConnection.Model.BasicAck(eventArgs.DeliveryTag, false);
                            return;
                        }
                    }

                    if (validationResult.Any())
                    {
                        _logger.LogError("{Queue} - Payload message validation failed, delivery tag: {DeliveryTag}, body: {@Body} and validation result: {@ValidationResult}",
                            nameof(_configuration.Queue), eventArgs.DeliveryTag, eventArgs.Body, validationResult);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Consumer failure - {Queue} delivery tag: {DeliveryTag} and body: {@Body}", nameof(_configuration.Queue), eventArgs.DeliveryTag, eventArgs.Body);
                }

                RejectMessage(eventArgs, rabbitConnection);
            };
        }

        private void RegisterEvents(IMQConnection rabbitConnection)
        {
            rabbitConnection.Connection.ConnectionBlocked += (state, args)
                => MQActiveConsumers.SetStatus(_configuration.Queue, MQConsumerStatus.Ready);

            rabbitConnection.Connection.ConnectionShutdown += (state, args)
                => MQActiveConsumers.SetStatus(_configuration.Queue, MQConsumerStatus.Stopped);

            rabbitConnection.Connection.RecoverySucceeded += (state, args)
                => MQActiveConsumers.SetStatus(_configuration.Queue, MQConsumerStatus.Ready);
        }

        private void RegisterQueue(EventingBasicConsumer consumer, IMQConnection rabbitConnection)
        {
            if (rabbitConnection.Model is not { } model)
                throw new BrokerUnreachableException(new Exception("MQ.IModel inaccessible model"));

            var arguments = new Dictionary<string, object> { { "x-queue-type", _configuration.MQType.XQueueType() } };
            if (_configuration.MessageTtl.HasValue)
                arguments.Add("x-message-ttl", _configuration.MessageTtl);

            try
            {
                model.QueueDeclare(_configuration.Queue, _configuration.QueueDurable ?? false,
                    _configuration.QueueExclusive ?? true, _configuration.QueueAutoDelete ?? true, arguments);
            }
            finally
            {
                model.BasicConsume(_configuration.Queue, _configuration.QueueAutoAck ?? false, consumer);
            }
        }

        private void RegisterExchange(EventingBasicConsumer consumer, IMQConnection rabbitConnection)
        {
            if (_configuration.Exchange is not { } exchange) return;

            if (rabbitConnection.Model is not { } model)
                throw new BrokerUnreachableException(new Exception("Rabbit.IModel inaccessible model"));

            try
            {
                model.ExchangeDeclare(exchange.Name, exchange.Type.XExchangeType(), exchange.Durable ?? false, exchange.AutoDelete ?? false, exchange.Arguments ?? null);
            }
            finally
            {
                model.ExchangeBind(exchange.Destination, exchange.Source, _configuration.Queue);
            }
        }

        private void RejectMessage(BasicDeliverEventArgs eventArgs, IMQConnection rabbitConnection)
        {
            rabbitConnection.Model.BasicNack(
                eventArgs.DeliveryTag, false, false);

            if (rabbitConnection.Retry)
                Retry(eventArgs, rabbitConnection);
        }

        private static void Retry(BasicDeliverEventArgs eventArgs, IMQConnection rabbitConnection)
        {
            object currentAttempt = 0;

            if (eventArgs.BasicProperties.Headers != null
                && eventArgs.BasicProperties.Headers.TryGetValue(RetryHeader, out object valueCurrentAttempt)
               ) currentAttempt = valueCurrentAttempt;

            var current = ((int)currentAttempt) + 1;
            if (current > rabbitConnection.RetryCount) return;

            var properties = rabbitConnection.Model.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object> { [RetryHeader] = current };
            properties.Persistent = true;

            rabbitConnection.Model.BasicPublish(eventArgs.Exchange, eventArgs.RoutingKey, properties, eventArgs.Body);
        }
    }
}
