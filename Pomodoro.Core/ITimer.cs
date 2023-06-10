namespace Pomodoro.Core;

public interface ITimer
{
    void Tick(Delegate handler);
}