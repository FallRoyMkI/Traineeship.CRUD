namespace WorkTest.Bll.Exceptions;

public class NotAllowToEditEntityException : Exception
{
    public NotAllowToEditEntityException(string message) : base(message) { }
}