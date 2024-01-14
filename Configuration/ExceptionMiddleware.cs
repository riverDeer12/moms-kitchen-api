using System.Net;
using MomsKitchen.Exceptions;

namespace MomsKitchen.Configuration;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    /// <summary>
    /// Method that intercepts http requests and catches
    /// possible exceptions. If no custom exceptions are caught,
    /// returns a 500 Internal Server Error.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.NotFound);
        }
        catch (BadRequestException ex)
        {
            await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    // Helper method that returns a custom ExceptionDetails model
    // which contains error's status code and message
    private Task HandleExceptionAsync(HttpContext context, Exception exception,
        int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(new ExceptionDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message
        }.ToString());
    }
}