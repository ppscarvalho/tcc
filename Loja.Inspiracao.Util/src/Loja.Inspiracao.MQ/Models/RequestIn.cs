using Loja.Inspiracao.MQ.Events;

namespace Loja.Inspiracao.MQ.Models
{
    public class RequestIn : IEvent
    {
        public string? Host { get; set; }
        public string? Queue { get; set; }
        public string? Result { get; set; }
    }
}
