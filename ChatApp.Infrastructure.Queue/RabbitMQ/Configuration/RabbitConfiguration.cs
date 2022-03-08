namespace ChatApp.Infrastructure.Queue.RabbitMQ.Configuration
{
    public class RabbitConfiguration : IRabbitConfiguration
    {
        public bool Enabled { get; set; }

        public string Exchange { get; set; }

        public string RoutingKey { get; set; }

        public string HostName { get; set; }

        public int Port { get; set; }

        public string VirtualHost { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string QueueName { get; set; }
    }
}