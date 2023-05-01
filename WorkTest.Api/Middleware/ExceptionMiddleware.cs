﻿using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using WorkTest.Constants;
using WorkTest.Constants.Exceptions;
using WorkTest.Models.Dto;

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
        string message = ex.Message;
        int code = ex switch
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
        ExceptionResponseDto exR = new(message,code);
        string result = JsonSerializer.Serialize(exR);
        context.Response.StatusCode = exR.Code;
        return context.Response.WriteAsync(result);
    }
}