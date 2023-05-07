#nullable disable

namespace Loja.Inspiracao.RabbitMQ.Options
{
    public class MQOptions
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public bool Retry { get; set; }
        public int RetryCount { get; set; }
    }
}
