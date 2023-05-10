using MassTransit;
using System;
using System.Threading.Tasks;

namespace Loja.Inspiracao.MQ.Operators
{
    public class Publish : IPublish
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBus _bus;

        public Publish(IPublishEndpoint publishEndpoint, IBus bus)
        {
            _publishEndpoint = publishEndpoint;
            _bus = bus;
        }

        public async Task DoPublish<T>(T obj)
        {
            await _publishEndpoint.Publish(obj);
        }


        public async Task DoSend<T>(T obj)
        {
            await _bus.Send(obj);
        }

        public async Task<TResponse> DoRPC<TRequest, TResponse>(TRequest obj, int timeout = 120) where TRequest : class where TResponse : class
        {
            var response = await _bus.Request<TRequest, TResponse>(obj, timeout: TimeSpan.FromSeconds(timeout));
            return response?.Message;
        }
    }
}
