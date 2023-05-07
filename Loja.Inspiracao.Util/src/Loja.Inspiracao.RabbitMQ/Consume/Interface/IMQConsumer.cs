using System.ComponentModel.DataAnnotations;

namespace Loja.Inspiracao.RabbitMQ.Consume.Interface
{
    public interface IMQConsumer
    {
        [Required]
        Guid Guid { get; set; }
    }
}
