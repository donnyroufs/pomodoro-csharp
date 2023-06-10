using Microsoft.Extensions.Configuration;
using Pomodori.CLI;
using Pomodori.Core;

var timer = new TimerImpl();
var pomodoro = new Pomodoro(timer);
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var logPath = config.GetValue<string>("LogPath");

if (logPath is null)
{
    throw new Exception("log path is required");
}

pomodoro.Attach(new LogObserver(new FileWriter(logPath)));
pomodoro.Start();

var viewModel = new ViewModel(pomodoro);

while (true)
{
    Console.CursorVisible = false;
    Console.Write(viewModel);
    Console.SetCursorPosition(0, 0);
}