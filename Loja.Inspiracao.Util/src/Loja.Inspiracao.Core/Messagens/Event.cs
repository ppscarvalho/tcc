#nullable disable
using MediatR;
using Loja.Inspiracao.Core.Messagens;

namespace Loja.Inspiracao.Core.Messagens
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }
        public string RoutingKey { get; private set; }
        public void SetRoutingKey(string name) => RoutingKey = name;
        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
