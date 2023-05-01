using Microsoft.EntityFrameworkCore;
using Npgsql;
using WorkTest.Constants.Exceptions;

namespace WorkTest.Constants;

public class ExceptionResponse
{
    public int Code { get; set; }
    public string Message { get; set; }

    public ExceptionResponse(Exception ex)
    {
        Message = ex.Message;
        Code = ex switch
        {
            StatusNotExistException => 400,
            NotAllowedToDeleteOrderException => 403,
            EntityNotFoundException => 404,
            NotAllowToEditEntityException => 406,
            AttemptToGetDeletedOrderException
            or OrderWithoutLinesException
            or LineWithNegativeOrZeroQuantityException
            or OrderGuidAlreadyExistException => 422,
            NpgsqlException
            or DbUpdateException => 500,
            _ => 400
        };
    }
}