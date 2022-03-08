using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Queue.RabbitMQ
{
    public interface IRabbitConsumer
    {
        Task<string> ConsumeAsync(string consumerIdentifier);
    }
}