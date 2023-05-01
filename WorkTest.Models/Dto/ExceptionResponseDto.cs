namespace WorkTest.Models.Dto;

public class ExceptionResponseDto
{
    public int Code { get; set; }
    public string Message { get; set; }

    public ExceptionResponseDto(string message, int code)
    {
        Message = message;
        Code = code;
    }
}