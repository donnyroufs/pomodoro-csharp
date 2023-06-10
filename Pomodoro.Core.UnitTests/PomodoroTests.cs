using FluentAssertions;
using Moq;

namespace Pomodoro.Core.UnitTests;

public class PomodoroShould
{
    [Test]
    public void BeInPendingStateWhenNotStarted()
    {
        var timer = new FakeTimer();
        Pomodoro pomodoro = new(timer);


        pomodoro
            .GetState()
            .Should()
            .Be(PomodoroState.Pending);
    }

    [Test]
    public void BeInWorkStateWhenStarted()
    {
        var timer = new FakeTimer();
        Pomodoro pomodoro = new(timer);

        pomodoro.Start();
        
        pomodoro
            .GetState()
            .Should()
            .Be(PomodoroState.Work);
    }

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

    [Test]
    public void SwitchFromWorkToShortBreak()
    {
        var timer = new FakeTimer();
        Pomodoro pomodoro = new(timer);

        pomodoro.Start();

        Enumerable
            .Range(0, 1500)
            .ToList()
            .ForEach(_ => { timer.SimulateClockTick(); });

        pomodoro
            .GetRemainingTime()
            .Should()
            .Be(300);

        pomodoro
            .GetState()
            .Should()
            .Be(PomodoroState.ShortBreak);
    }
}