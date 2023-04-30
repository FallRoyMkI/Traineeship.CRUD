namespace WorkTest.Constants.Exceptions;

public class NotAllowToEditEntityException : Exception
{
    public NotAllowToEditEntityException(string message) : base(message) { }
}