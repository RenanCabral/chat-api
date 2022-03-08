using ChatApp.DataContracts;
using ChatApp.Infrastructure.Queue.RabbitMQ;
using System;
using System.Threading.Tasks;

namespace ChatApp.Application.Services
{
    public class LoginService : ILoginService
    {
        private IRabbitConsumer rabbitConsumer;

        public LoginService(IRabbitConsumer rabbitConsumer)
        {
            this.rabbitConsumer = rabbitConsumer;
        }

        public async Task LogInAsync(User user)
        {
            var message = await this.rabbitConsumer.ConsumeAsync(user.Email);
        }
    }
}