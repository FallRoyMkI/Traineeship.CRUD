namespace WorkTest.Validator.Exceptions;

public class DifferentLinesWithSameProductException : Exception
{
    public DifferentLinesWithSameProductException(string message) : base(message) { }
}