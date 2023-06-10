namespace Pomodori.Core;

public interface ITimer
{
    void Tick(Delegate? handler);
}
