using FluentAssertions;
using Moq;

namespace Pomodoro.Core.UnitTests;

public class PomodoroShould
{
    [Test]
    public void DecrementTheRemainingTimeEveryClockTick()
    {
        var timer = new FakeTimer();
        Pomodoro pomodoro = new(timer);
        
        pomodoro.Start();
        
        timer.SimulateClockTick();

        pomodoro
            .GetRemainingTime()
            .Should()
            .Be(1499);
        
        timer.SimulateClockTick();
        
        pomodoro
            .GetRemainingTime()
            .Should()
            .Be(1498);
    }
}