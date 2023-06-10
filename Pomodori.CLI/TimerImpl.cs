using Pomodori.Core;
using Timer = System.Timers.Timer;

namespace Pomodori.CLI;

public class TimerImpl : ITimer
{
    private readonly Timer _timer = new Timer(1000);
    
    public void Tick(Delegate? handler)
    {
        _timer.Elapsed += (sender, e) =>
        {
            handler?.DynamicInvoke();
        };
        
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }
}