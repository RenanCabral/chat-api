namespace ChatApp.CrossCutting
{
    public static class AppConfiguration
    {
        public static Settings Settings { get; set; } = new Settings();
        public static RabbitMqConfiguration RabbitMqConfiguration { get; set; } = new RabbitMqConfiguration();
    }

    public class Settings
    {
   
    }

    public class RabbitMqConfiguration
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
    }
}