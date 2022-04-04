namespace ChatApp.Web.Hubs
{
    public interface IChatHub
    {
        Task SendMessage(string user, string message);
    }
}
