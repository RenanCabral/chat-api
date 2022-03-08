using ChatApp.Infrastructure.Queue.RabbitMQ.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Queue.RabbitMQ
{
    public class RabbitConsumer : IRabbitConsumer
    {
        public RabbitConsumer(IRabbitConfiguration configuration)
        {
            this.configuration = configuration;

            this.factory = new ConnectionFactory
            {
                HostName = this.configuration.HostName,
                Port = this.configuration.Port,
                VirtualHost = this.configuration.VirtualHost,
                UserName = this.configuration.Username,
                Password = this.configuration.Password
            };

            this.currentConnectionId = Guid.NewGuid().ToString();

            CreateConnection();
        }

        #region Fields

        private IRabbitConfiguration configuration;
        private ConnectionFactory factory;
        private IModel model;

        private IConnection connection;
        private string clientProviderName = "Default";
        private string currentConnectionId;

        private IBasicProperties properties;

        #endregion

        #region Public Methods

        public async Task<string> ConsumeAsync(string consumerIdentifier)
        {
            var message = string.Empty;

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: consumerIdentifier,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                    };


                    message = await Task.FromResult(channel.BasicConsume(queue: consumerIdentifier, autoAck: true, consumer: consumer));
                }
            }

            return message;
        }

        #endregion

        #region Private Methods

        private void CreateBasicPropperties()
        {
            if (properties == null)
            {
                properties = model.CreateBasicProperties();
            }
            properties.DeliveryMode = 2;
        }

        private void CreateConnection()
        {
            this.connection = this.factory.CreateConnection(clientProviderName);

            this.model = this.connection.CreateModel();

            this.currentConnectionId = Guid.NewGuid().ToString();
        }

        #endregion
    }
}