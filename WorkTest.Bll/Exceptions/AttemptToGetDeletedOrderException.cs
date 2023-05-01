namespace WorkTest.Bll.Exceptions;

public class AttemptToGetDeletedOrderException : Exception
{
    public AttemptToGetDeletedOrderException(string message) : base(message) { }
}