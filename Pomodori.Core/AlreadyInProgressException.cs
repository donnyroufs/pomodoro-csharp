namespace Pomodori.Core;

public class AlreadyInProgressException : Exception
{
    public AlreadyInProgressException() : base("You cannot start an already in progress Pomodoro")
    {
    }
}