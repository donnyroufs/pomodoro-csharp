using Pomodori.Core;

namespace Pomodori.Core.UnitTests;

public class FakeTimer : ITimer
{
    private Delegate? _handler;

    public void SimulateClockTick()
    {
        _handler?.DynamicInvoke();
    }

    public void Tick(Delegate? handler)
    {
        _handler = handler;
    }

    public void SimulateWork()
    {
        Enumerable
            .Range(0, Pomodoro.WorkInterval * 60)
            .ToList()
            .ForEach(_ => { SimulateClockTick(); });
    }

    public void SimulateShortBreak()
    {
        Enumerable
            .Range(0, Pomodoro.ShortBreakInterval * 60)
            .ToList()
            .ForEach(_ => { SimulateClockTick(); });
    }


    public void SimulateLongBreak()
    {
        Enumerable
            .Range(0, Pomodoro.LongBreakInterval * 60)
            .ToList()
            .ForEach(_ => { SimulateClockTick(); });
    }
}