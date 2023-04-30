namespace WorkTest.Constants.Exceptions;

public class StatusNotExistException : Exception
{
    public StatusNotExistException(string message) : base(message) { }
}