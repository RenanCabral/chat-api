using ChatApp.DataContracts;
using ChatApp.Infrastructure.Queue.RabbitMQ;
using System.Threading.Tasks;

namespace ChatApp.Application.Services
{
    public class MessageService : IMessageService
    {
        private IRabbitProducer rabbitProducer;

        public MessageService(IRabbitProducer rabbitProducer)
        {
            this.rabbitProducer = rabbitProducer;
        }

        public async Task SendAsync(CommandMessage message)
        {
            await this.rabbitProducer.PublishAsync(message);
        }
    }
}
