namespace Pomodoro.Core.UnitTests;

internal class FakeTimer : ITimer
{
    private Delegate _handler;

    public void SimulateClockTick()
    {
        _handler.DynamicInvoke();
    }
    
    public void Tick(Delegate handler)
    {
        _handler = handler;
    }
}