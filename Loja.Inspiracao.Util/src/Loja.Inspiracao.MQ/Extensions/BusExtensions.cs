#nullable disable

using Loja.Inspiracao.MQ.Configuration;
using Loja.Inspiracao.MQ.Events;
using Loja.Inspiracao.MQ.Operators;
using Loja.Inspiracao.Util.Exceptions;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net;

namespace Loja.Inspiracao.MQ.Extensions
{
    public static class BusExtensions
    {
        public static void AddEventBus(this IServiceCollection services, BuilderBus builder)
        {
            services.AddObserver(builder);

            services.AddMassTransit(bus =>
            {
                AddConsumers(bus, builder);

                bus.UsingRabbitMq((context, config) =>
                {
                    config.ConfigureNewtonsoftJson();

                    config.Host(builder.ConnectionString);

                    AddMessageRetry(config, builder);
                    AddPublishers(context, config, builder);

                    AddReceiveEndpoint(context, config, builder);
                });
            });

            services.AddDependency();
        }

        private static Func<JsonSerializerSettings, JsonSerializerSettings> ConfigureNewtonsoftJson(this IBusFactoryConfigurator config)
        {
            var settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            Func<JsonSerializerSettings, JsonSerializerSettings> configure = x => settings;

            config.UseNewtonsoftJsonSerializer();
            config.ConfigureNewtonsoftJsonSerializer(configure);

            return configure;
        }

        private static void AddObserver(this IServiceCollection services, BuilderBus builder)
        {
            if (builder?.Consumers?.Any() is true)
            {
                services.AddConsumeObserver<ConsumeObserver>();
            }
        }

        private static void AddConsumers(IBusRegistrationConfigurator bus, BuilderBus builder)
        {
            if (builder?.Consumers?.Any() is true)
            {
                bus.AddConsumers(builder.Consumers.Select(s => s.TypeConsumer).ToArray());
            }
        }

        private static void AddMessageRetry(IRabbitMqBusFactoryConfigurator config, BuilderBus builder)
        {
            if (builder.Retry is null) return;
            config.UseMessageRetry(
                r =>
                    {
                        r.Interval(builder.Retry.RetryCount, builder.Retry.Interval);
                    }
                );
        }

        private static void AddPublishers(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator config, BuilderBus builder)
        {
            if (builder?.Publishers?.Any() is true)
            {
                foreach (var publish in builder.Publishers)
                {
                    VerifyQueuePublisher(publish, builder);

                    publish.Config(config);
                }
            }
        }

        private static void VerifyQueuePublisher(IPublisher publisher, BuilderBus builder)
        {
            if (builder.Publishers.Count(c => c.Queue.Equals(publisher.Queue)) > 1)
            {
                throw new ApiException($"Queue: {publisher.Queue} duplicate!", HttpStatusCode.BadRequest);
            }
        }

        private static void AddReceiveEndpoint(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator config, BuilderBus builder)
        {
            if (builder?.Consumers?.Any() is true)
            {
                foreach (var consumer in builder.Consumers)
                {
                    VerifyQueueConsumer(consumer, builder);

                    config.ReceiveEndpoint(consumer.Queue, c =>
                    {
                        if (consumer.QuorumQueue)
                            c.SetQuorumQueue();

                        if (string.IsNullOrEmpty(consumer.ExchangeName) is false && string.IsNullOrEmpty(consumer.RoutingKey) is false && string.IsNullOrEmpty(consumer.ExchangeType) is false)
                        {
                            c.Bind(consumer.ExchangeName, x =>
                            {
                                x.Durable = consumer.Durable;
                                x.AutoDelete = consumer.AutoDelete;
                                x.RoutingKey = consumer.RoutingKey;
                                x.ExchangeType = consumer.ExchangeType;
                            });
                        }

                        c.AutoDelete = consumer.AutoDelete;
                        c.Durable = consumer.Durable;

                        c.ConfigureConsumer(context, consumer.TypeConsumer);
                    });
                }
            }
        }

        private static void VerifyQueueConsumer(Consumer consumer, BuilderBus builder)
        {
            if (builder.Consumers.Count(c => c.Queue.Equals(consumer.Queue)) > 1)
            {
                throw new ApiException($"Queue: {consumer.Queue} duplicate!", HttpStatusCode.BadRequest);
            }
        }

        private static void AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IPublish, Publish>();
            services.AddScoped<INotificationEvent, NotificationEvent>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}

