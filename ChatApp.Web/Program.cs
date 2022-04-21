using ChatApp.Application.Services;
using ChatApp.CrossCutting;
using ChatApp.MessageQueue.Brokers.RabbitMQ;
using ChatApp.Web.Hubs;
using ChatApp.Web.Jobs;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSignalR();

builder.Services.AddControllers();

builder.Services.AddQuartz();

// Dependencies injection
builder.Services.AddScoped<IConsumer>(rabbitReceiver => new Consumer(AppConfiguration.RabbitMqConfiguration));

builder.Services.AddScoped<IProducer>(rabbitPublisher => new Producer(AppConfiguration.RabbitMqConfiguration));

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddSingleton<IChatHub, ChatHub>();

builder.Services.UseScheduler();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");

var env = builder.Environment;

var configuration = builder.Configuration;

configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{ env.EnvironmentName }.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

AppConfiguration.RabbitMqConfiguration = new RabbitMqConfiguration()
    {
        HostName = configuration.GetValue<string>("Settings:RabbitMq:HostName"),
        UserName = configuration.GetValue<string>("Settings:RabbitMq:UserName"),
        Password = configuration.GetValue<string>("Settings:RabbitMq:Password"),
        VirtualHost = configuration.GetValue<string>("Settings:RabbitMq:VirtualHost"),
        Port = configuration.GetValue<int>("Settings:RabbitMq:Port")
    };

app.Run();