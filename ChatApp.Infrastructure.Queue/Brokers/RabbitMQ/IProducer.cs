namespace ChatApp.MessageQueue.Brokers.RabbitMQ
{
    public interface IProducer
    {
        void Publish(string exchange, string message);
    }
}
