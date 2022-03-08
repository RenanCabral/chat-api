using ChatApp.Application.Services;
using ChatApp.Infrastructure.CrossCutting;
using ChatApp.Infrastructure.Queue.RabbitMQ;
using ChatApp.Infrastructure.Queue.RabbitMQ.Configuration;
using ChatApp.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSignalR();

builder.Services.AddControllers();

// Dependencies injection
builder.Services.AddSingleton<IRabbitProducer>(svc => new RabbitProducer(GetRabbitConfiguration()));
builder.Services.AddSingleton<IRabbitConsumer>(svc => new RabbitConsumer(GetRabbitConfiguration()));
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IMessageService, MessageService>();

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

builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{ env.EnvironmentName }.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

AppConfiguration.Load(builder.Configuration);


app.Run();


RabbitConfiguration GetRabbitConfiguration() =>
new RabbitConfiguration
{
    HostName = AppConfiguration.RabbitMQ["HostName"],
    Port = Convert.ToInt32(AppConfiguration.RabbitMQ["Port"]),
    VirtualHost = AppConfiguration.RabbitMQ["VirtualHost"],
    Username = AppConfiguration.RabbitMQ["Username"],
    Password = AppConfiguration.RabbitMQ["Password"],
    Exchange = AppConfiguration.RabbitMQ["Exchange"],
    RoutingKey = AppConfiguration.RabbitMQ["RoutingKey"]
};