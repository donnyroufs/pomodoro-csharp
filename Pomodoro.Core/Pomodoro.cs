namespace Pomodoro.Core;

public class Pomodoro
{
    public const int WorkInterval = 25;
    public const int ShortBreakInterval = 5;
    public const int LongBreakInterval = 20;
    
    private TimeSpan _time = TimeSpan.FromMinutes(WorkInterval);
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
        if (_state != PomodoroState.Pending)
        {
            throw new AlreadyInProgressException();
        }
        
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
            SetState(PomodoroState.LongBreak, TimeSpan.FromMinutes(LongBreakInterval));
            return;
        }

        SetState(PomodoroState.ShortBreak, TimeSpan.FromMinutes(ShortBreakInterval));
    }

    private void HandleBreakState()
    {
        SetState(PomodoroState.Work, TimeSpan.FromMinutes(WorkInterval));
    }

    private void SetState(PomodoroState state, TimeSpan time)
    {
        _state = state;
        _time = time;
    }
}