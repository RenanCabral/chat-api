using ChatApp.DataContracts;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;

namespace ChatApp.Application.Services
{
    public interface IMessageService
    {
        Task SendAsync(CommandMessage message);

        void ReadMessagesFromQueue(string queue, EventHandler<BasicDeliverEventArgs> onMessageReceived);
    }
}