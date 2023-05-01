namespace WorkTest.Validator.Exceptions;

public class LineWithNegativeOrZeroQuantityException : Exception
{
    public LineWithNegativeOrZeroQuantityException(string message) : base(message) { }
}