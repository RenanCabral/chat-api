using RabbitMQ.Client.Events;
using System;

namespace ChatApp.MessageQueue.Brokers.RabbitMQ
{
    public interface IConsumer
    {
        void Consume(string queue, EventHandler<BasicDeliverEventArgs> eventHandler);
    }
}
