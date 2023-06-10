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
}