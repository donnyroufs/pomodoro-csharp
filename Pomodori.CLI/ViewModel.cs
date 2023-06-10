using System.Text;

namespace Pomodori.CLI;

public class ViewModel
{
    private readonly Core.Pomodoro _pomodoro;

    public ViewModel(Core.Pomodoro pomodoro)
    {
        _pomodoro = pomodoro;
    }
    
    public override string ToString()
    {
        var builder = new StringBuilder();

        return builder
            .AppendLine("--------------------------------------------")
            .AppendLine(
                $"[{_pomodoro.GetState()}] Remaining time: {TimeSpan.FromSeconds(_pomodoro.GetRemainingTime())}")
            .AppendLine("--------------------------------------------")
            .ToString();
    }
}