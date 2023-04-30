namespace WorkTest.Constants.Exceptions;

public class NotAllowedToDeleteOrderException : Exception
{
    public NotAllowedToDeleteOrderException(string message) : base(message) { }
}