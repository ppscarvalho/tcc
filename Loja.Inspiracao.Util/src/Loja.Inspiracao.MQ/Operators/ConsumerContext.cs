using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Inspiracao.MQ.Operators
{
    public class ConsumerContext<T> where T : class
    {
        private ConsumeContext<T> _context;

        public ConsumerContext(ConsumeContext<T> context)
        {
            _context = context;
        }

        public T Message { get => _context.Message; }

        public Guid? MessageId { get => _context.MessageId; }

        public Guid? RequestId { get => _context.RequestId; }

        public Guid? CorrelationId { get => _context.CorrelationId; }

        public Guid? ConversationId { get => _context.ConversationId; }

        public Guid? InitiatorId { get => _context.InitiatorId; }

        public DateTime? ExpirationTime { get => _context.ExpirationTime; }

        public Uri SourceAddress { get => _context.SourceAddress; }

        public Uri DestinationAddress { get => _context.DestinationAddress; }

        public Uri ResponseAddress { get => _context.ResponseAddress; }

        public Uri FaultAddress { get => _context.FaultAddress; }

        public DateTime? SentTime { get => _context.SentTime; }

        public Headers Headers { get => _context.Headers; }

        public HostInfo Host { get => _context.Host; }

        public ReceiveContext ReceiveContext { get => _context.ReceiveContext; }

        public SerializerContext SerializerContext { get => _context.SerializerContext; }

        public Task ConsumeCompleted { get => _context.ConsumeCompleted; }

        public IEnumerable<string> SupportedMessageTypes { get => _context.SupportedMessageTypes; }

        public async Task RespondAsync<R>(R message)
        {
            await _context.RespondAsync(message);
        }
    }
}
