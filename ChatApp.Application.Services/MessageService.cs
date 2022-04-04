using ChatApp.DataContracts;
using ChatApp.MessageQueue.Brokers.RabbitMQ;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;

namespace ChatApp.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IProducer producer;
        private readonly IConsumer consumer;

        public MessageService(IProducer producer, IConsumer consumer)
            => (this.producer, this.consumer) = (producer, consumer);

        public async Task SendAsync(CommandMessage message)
        {
            this.producer.Publish("chat-app-bot-exchange", message.Parameter);
        }

        public void ReadMessagesFromQueue(string queue, EventHandler<BasicDeliverEventArgs> onMessageReceived)
        {
            this.consumer.Consume(queue, onMessageReceived);
        }

    }
}
