using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Queue.RabbitMQ
{
    public interface IRabbitProducer
    {
        Task PublishAsync<TIn>(TIn data);
    }
}
