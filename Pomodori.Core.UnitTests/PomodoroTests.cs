using FluentAssertions;
using Moq;

namespace Pomodori.Core.UnitTests;

public class PomodoroShould
{
    public FakeTimer timer;
    public Pomodoro pomodoro;

    [SetUp]
    public void Setup()
    {
        timer = new FakeTimer();
        pomodoro = new Pomodoro(timer);
    }

    [Test]
    public void BeInPendingStateWhenNotStarted()
    {
        pomodoro
            .GetState()
            .Should()
            .Be(PomodoroState.Pending);
    }

    [Test]
    public void BeInWorkStateWhenStarted()
    {
        pomodoro.Start();

        pomodoro
            .GetState()
            .Should()
            .Be(PomodoroState.Work);
    }

    [Test]
    public void SwitchFromWorkToShortBreak()
    {
        pomodoro.Start();

        timer.SimulateWork();

        pomodoro
            .GetRemainingTime()
            .Should()
            .Be(ToSeconds(Pomodoro.ShortBreakInterval));

        pomodoro
            .GetState()
            .Should()
            .Be(PomodoroState.ShortBreak);
    }

    [Test]
    public void SwitchFromShortBreakToWorkIfNotExceededThreeCycles()
    {
        pomodoro.Start();

        timer.SimulateWork();
        timer.SimulateShortBreak();

        pomodoro
            .GetRemainingTime()
            .Should()
            .Be(ToSeconds(Pomodoro.WorkInterval));

        pomodoro
            .GetState()
            .Should()
            .Be(PomodoroState.Work);
    }

    [Test]
    public void SwitchFromWorkToLongBreakWhenExceededThreeCycles()
    {
        pomodoro.Start();

        timer.SimulateWork();
        timer.SimulateShortBreak();
        timer.SimulateWork();
        timer.SimulateShortBreak();
        timer.SimulateWork();

        pomodoro
            .GetRemainingTime()
            .Should()
            .Be(ToSeconds(Pomodoro.LongBreakInterval));

        pomodoro
            .GetState()
            .Should()
            .Be(PomodoroState.LongBreak);
    }

    [Test]
    public void SwitchToWorkStateAfterLongBreak()
    {
        pomodoro.Start();

        timer.SimulateWork();
        timer.SimulateShortBreak();
        timer.SimulateWork();
        timer.SimulateShortBreak();
        timer.SimulateWork();
        timer.SimulateLongBreak();

        pomodoro
            .GetRemainingTime()
            .Should()
            .Be(ToSeconds(Pomodoro.WorkInterval));

        pomodoro
            .GetState()
            .Should()
            .Be(PomodoroState.Work);
    }

    [Test]
    public void NotBeAbleToStartAPomodoroIfInProgress()
    {
        pomodoro.Start();

        var act = () => pomodoro.Start();

        act.Should().Throw<AlreadyInProgressException>();
    }

    [Test]
    public void DispatchesStateChangedEvent_OnStateChange()
    {
        var mockObserver = new Mock<IObserver<PomodoroStateChangedData>>();
        pomodoro.Attach(mockObserver.Object);
        pomodoro.Start();
        timer.SimulateWork();

        mockObserver
            .Verify(x =>
                    x.OnNext(new PomodoroStateChangedData(PomodoroState.Work, PomodoroState.ShortBreak)),
                Times.Once);
    }

    [Test]
    public void DispatchesStateChangedEvent_OnStart()
    {
        var mockObserver = new Mock<IObserver<PomodoroStateChangedData>>();
        pomodoro.Attach(mockObserver.Object);
        pomodoro.Start();

        mockObserver
            .Verify(x =>
                    x.OnNext(new PomodoroStateChangedData(PomodoroState.Pending, PomodoroState.Work)),
                Times.Once);
    }

    private static int ToSeconds(int minutes)
    {
        return minutes * 60;
    }
}