using ChatApp.Infrastructure.Queue.RabbitMQ.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.Queue.RabbitMQ
{
    public class RabbitProducer : IRabbitProducer
    {
        #region Constructors

        public RabbitProducer(IRabbitConfiguration configuration)
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

        #endregion

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

        public Task PublishAsync<TIn>(TIn data)
        {
            return Task.Run(() =>
            {
                var result = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));

                var currentConnectionId = this.currentConnectionId;

                try
                {
                    CreateBasicPropperties();

                    this.model.BasicPublish(configuration.Exchange, configuration.RoutingKey, properties, result);
                }
                catch (Exception ex)
                {
                    //TODO: exception handling code here
                }
            });
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