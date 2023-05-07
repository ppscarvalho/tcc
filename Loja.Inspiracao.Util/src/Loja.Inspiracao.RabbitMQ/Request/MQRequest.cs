#nullable disable

using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Loja.Inspiracao.RabbitMQ.Request
{
    public class MQRequest
    {
        /// <summary>
        /// Retry Count
        /// </summary>
        public int RetryCount { get; set; } = 3;

        /// <summary>
        /// Exchange
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// Rota
        /// </summary>
        public string RoutingKey { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public object Message { get; set; }

        /// <summary>
        /// Message Properties
        /// </summary>
        public IBasicProperties Properties { get; set; }

        /// <summary>
        /// Body [Bytes]
        /// </summary>
        public byte[] Body => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Message));
    }
}
