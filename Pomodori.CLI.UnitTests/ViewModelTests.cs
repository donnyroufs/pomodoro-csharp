using FluentAssertions;
using Pomodori.Core;

namespace Pomodori.CLI.UnitTests;

public class ViewModelShould
{
    [Test]
    public void ReturnTheRemainingTime()
    {
        var timer = new Timer();
        var pomodoro = new Pomodori.Core.Pomodoro(timer);
        var vm = new ViewModel(pomodoro);

        vm
            .ToString()
            .Should()
            .Contain("[Pending] Remaining time: 00:25:00");
    }
}

internal class Timer : ITimer
{
    public void Tick(Delegate? handler)
    {
        handler?.DynamicInvoke();
    }
}