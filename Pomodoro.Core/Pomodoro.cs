namespace Pomodoro.Core;

public class Pomodoro
{
    private TimeSpan _time = TimeSpan.FromMinutes(25);
    private PomodoroState _state = PomodoroState.Pending;
    private int _cycles;
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
        _timer.Tick(OnTick);
    }

    public PomodoroState GetState()
    {
        return _state;
    }

    private void OnTick()
    {
        _time = _time.Subtract(TimeSpan.FromSeconds(1));

        if (!_time.Equals(TimeSpan.Zero)) return;

        if (_state == PomodoroState.Work)
        {
            HandleWorkState();
            return;
        }

        HandleBreakState();
    }

    private void HandleWorkState()
    {
        _cycles += 1;

        if (_cycles % 3 == 0)
        {
            SetState(PomodoroState.LongBreak, TimeSpan.FromMinutes(20));
            return;
        }

        SetState(PomodoroState.ShortBreak, TimeSpan.FromMinutes(5));
    }

    private void HandleBreakState()
    {
        SetState(PomodoroState.Work, TimeSpan.FromMinutes(25));
    }

    private void SetState(PomodoroState state, TimeSpan time)
    {
        _state = state;
        _time = time;
    }
}

public enum PomodoroState
{
    ShortBreak,
    Pending,
    Work,
    LongBreak
}