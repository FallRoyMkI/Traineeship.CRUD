namespace WorkTest.Validator.Exceptions;

public class OrderWithoutLinesException : Exception
{
    public OrderWithoutLinesException(string message) : base(message) { }
}