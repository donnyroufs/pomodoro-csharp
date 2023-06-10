using Pomodori.Core;

namespace Pomodori.CLI;

public class LogObserver : IObserver<PomodoroStateChangedData>
{
    private readonly IFileWriter _fileWriter;

    public LogObserver(IFileWriter fileWriter)
    {
        _fileWriter = fileWriter;
    }
    
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(PomodoroStateChangedData value)
    {
        _fileWriter.Write($"State changed from {value.PreviousState} to {value.CurrentState}.\n");
    }
}