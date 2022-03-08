using ChatApp.DataContracts;
using System.Threading.Tasks;

namespace ChatApp.Application.Services
{
    public interface ILoginService
    {
        Task LogInAsync(User user);
    }
}