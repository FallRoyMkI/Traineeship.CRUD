using System.Text.Json;
using WorkTest.Constants;

namespace WorkTest.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandlerExceptionAsync(context, ex);
        }
    }

    private static Task HandlerExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        ExceptionResponse exR = new(ex);
        string result = JsonSerializer.Serialize(exR);
        context.Response.StatusCode = exR.Code;
        return context.Response.WriteAsync(result);
    }
}