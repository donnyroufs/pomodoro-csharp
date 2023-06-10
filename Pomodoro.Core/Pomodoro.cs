namespace Pomodoro.Core;

public class Pomodoro
{
    private TimeSpan _time = TimeSpan.FromMinutes(25);
    private readonly ITimer _timer;

    public Pomodoro(ITimer timer)
    {
        _timer = timer;
    }

    public double GetRemainingTime()
    {
        return _time.TotalSeconds;
    }

    public void Start()
    {
        _timer.Tick(() =>
        {
            _time = _time.Subtract(TimeSpan.FromSeconds(1));
        });
    }
}