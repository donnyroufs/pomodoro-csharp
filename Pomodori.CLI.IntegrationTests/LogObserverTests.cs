using FluentAssertions;
using Pomodori.Core;

namespace Pomodori.CLI.IntegrationTests;

public class LogObserverShould
{
    public readonly string Path = Directory.GetCurrentDirectory() + @"\" + "logs.txt";

    [SetUp]
    [TearDown]
    public void TearDown()
    {
        File.Delete(Path);
    }

    [Test]
    public void WriteStateChangesToLogFile()
    {
        var fileWriter = new FileWriter(Path);
        var logObserver = new LogObserver(fileWriter);

        logObserver
            .OnNext(new PomodoroStateChangedData(PomodoroState.Pending, PomodoroState.Work));

        File
            .ReadLines(Path)
            .Should()
            .Contain("State changed from Pending to Work.");
    }

    [Test]
    public void AppendLogs()
    {
        var fileWriter = new FileWriter(Path);
        var logObserver = new LogObserver(fileWriter);

        logObserver
            .OnNext(new PomodoroStateChangedData(PomodoroState.Pending, PomodoroState.Work));
        logObserver
            .OnNext(new PomodoroStateChangedData(PomodoroState.Work, PomodoroState.ShortBreak));

        File
            .ReadLines(Path)
            .Should()
            .Contain("State changed from Pending to Work.")
            .And
            .Contain("State changed from Work to ShortBreak.");
    }
}