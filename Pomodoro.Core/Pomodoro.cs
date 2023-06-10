namespace Pomodoro.Core;

public class Pomodoro
{
    private TimeSpan _time = TimeSpan.FromMinutes(25);
    private PomodoroState _state = PomodoroState.Pending;
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
        _state = PomodoroState.Work;
        
        _timer.Tick(() =>
        {
            _time = _time.Subtract(TimeSpan.FromSeconds(1));

            if (_time.Equals(TimeSpan.Zero))
            {
                if (_state == PomodoroState.Work)
                {
                    _state = PomodoroState.ShortBreak;
                    _time = TimeSpan.FromMinutes(5);
                }
                else
                {
                    _state = PomodoroState.Work;
                }
            }
        });
    }

    public PomodoroState GetState()
    {
        return _state;
    }
}

public enum PomodoroState
{
    ShortBreak,
    Pending,
    Work
}