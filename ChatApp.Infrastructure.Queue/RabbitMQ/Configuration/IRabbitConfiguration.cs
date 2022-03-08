namespace ChatApp.Infrastructure.Queue.RabbitMQ.Configuration
{
    public interface IRabbitConfiguration
    {
        bool Enabled { get; }

        string Exchange { get; }

        string RoutingKey { get; }

        string HostName { get; }

        int Port { get; }

        string VirtualHost { get; }

        string Username { get; }

        string Password { get; }

        string QueueName { get; }
    }
}
