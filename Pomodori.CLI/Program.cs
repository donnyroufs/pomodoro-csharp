using Pomodori.CLI;
using Pomodori.Core;

var timer = new TimerImpl();
var pomodoro = new Pomodoro(timer);

pomodoro.Start();

var viewModel = new ViewModel(pomodoro);

while (true)
{
    Console.CursorVisible = false;
    Console.Write(viewModel);
    Console.SetCursorPosition(0, 0);
}