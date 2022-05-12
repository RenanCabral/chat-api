using ChatApp.Application.Services;
using ChatApp.Web.Hubs;
using Quartz;
using RabbitMQ.Client.Events;
using System.Text;

namespace ChatApp.Web.Jobs
{
    public class StockQuoteJob : IJob
    {
        private readonly IMessageService messageService;

        private readonly IChatHub chatHub;

        public StockQuoteJob(IMessageService messageService, IChatHub chatHub) 
            => (this.messageService, this.chatHub) = (messageService, chatHub);

        public Task Execute(IJobExecutionContext context)
        {
            messageService.ReadMessagesFromQueue("chat-app-queue", OnMessageReceived);

            return Task.CompletedTask;
        }

        private void OnMessageReceived(object sender, BasicDeliverEventArgs e)
        {
            var message = ReadMessageFromBody(e.Body);

            try
            {
                this.chatHub.SendMessage("chat-bot", message);
            }
            catch (Exception)
            {
                //TODO: some log here
            }
        }

        private string ReadMessageFromBody(ReadOnlyMemory<byte> body)
        {
            return Encoding.UTF8.GetString(body.ToArray());
        }
    }
}