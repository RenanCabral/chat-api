# chat-app
Small real time chat application where users can interact in a chatroom.

### Stack:

 * VisualStudio 2022;
 * C# 10;
 * Dotnet 6;
 * Asp.Net Core;
 * SignalR;
 * RabbitMQ;
 * Docker.

### How to start:

 * docker run -p 15672:15672 -p 5672:5672 -d --hostname rabbitmq --name rabbitmq rabbitmq:management

 * dotnet run --project ChatApp.Web\ChatApp.Web.csproj

 * http://localhost:5158/chatroom
