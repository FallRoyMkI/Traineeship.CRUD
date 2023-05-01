namespace WorkTest.Bll.Exceptions;

public class NotAllowedToDeleteOrderException : Exception
{
    public NotAllowedToDeleteOrderException(string message) : base(message) { }
}