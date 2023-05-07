using Loja.Inspiracao.Core.Messagens;

namespace Loja.Inspiracao.Core.Messagens.CommonMessage.DomainEvents
{
    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
