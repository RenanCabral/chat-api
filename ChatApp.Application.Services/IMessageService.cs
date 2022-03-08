using ChatApp.DataContracts;
using System.Threading.Tasks;

namespace ChatApp.Application.Services
{
    public interface IMessageService
    {
        Task SendAsync(CommandMessage message);
    }
}