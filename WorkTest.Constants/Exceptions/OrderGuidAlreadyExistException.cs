namespace WorkTest.Constants.Exceptions;

public class OrderGuidAlreadyExistException : Exception
{
    public OrderGuidAlreadyExistException(string message) : base(message) { }
}