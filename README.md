# BlazorAppChat

* .NET 7
* .EF Core
* MSSQL 2019
* SignalR 
* xUnits

To run the project will be necessary a Visual Studio 2022 community, .NET 7, and Microsoft SQL Server 2019 installed.
* First step, run the script on root of the solution project /Scripts/DatabaseCreation.sql in your local database
* Change the connection string changing your user and password database on files appsettings.json in both projects:
  * AppChat.Server 
  * BlazorAppChat
* Set multiple projects as startup projects:
  * AppChat.Server 
  * BlazorAppChat
  * ChatApp.Bots.StockApi
* After that you just need to run the project and everything should works fine.

In this challenge I developed a simple chat using an ASP.NET Core Server to run the chat and register accounts and bot commands.
Instead of use a service bus I prefer a webclient based bot, communicating via webhooks to save some resources, and to not push any extra infrastructure via docker.
But in this approach we can create other bot implemations that can register as a plugins. In this example we need to create a bot command using the swagger using the following pattern:


```json
{
  "uri": "https://localhost:7270/api/stock",
  "replayTo": "http://localhost:5185/api/chatbot/replay",
  "name": "stock-info",
  "command": "^\\/([^\\s@]\u002B)?=(\\S\u002B)?\\s?(.*)$"
}
```

Where:

```
Uri: job background service
ReplayTo: chat enpoint used to receive and enqueue a message to replay to the requester
Name: name of the command
Command: Regex pattern used by the bot interceptor on the chathub
```

Unfortunally right now is a simple openned chat without security. 
``` C#
// TODO: add authentication + authorization
```

And a simple test to show how we are implementing ddd tecniques
