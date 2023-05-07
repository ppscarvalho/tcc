namespace Loja.Inspiracao.RabbitMQ.Consume
{
    /// <summary>
    /// Consumer Status
    /// </summary>
    public enum MQConsumerStatus
    {
        /// <summary>
        /// Waiting Broker
        /// </summary>
        WaitingBroker,

        /// <summary>
        /// Ready!
        /// </summary>
        Ready,

        /// <summary>
        /// Running
        /// </summary>
        Running,

        /// <summary>
        /// Stopped
        /// </summary>
        Stopped
    }
}
