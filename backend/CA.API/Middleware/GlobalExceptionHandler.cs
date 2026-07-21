using System.Net;
using System.Text.Json;
using CA.DAL.Exceptions;

namespace CA.API.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            DomainException => (int)HttpStatusCode.BadRequest,
            ForbiddenException => (int)HttpStatusCode.Forbidden,
            NotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = statusCode;

        var response = new
        {
            status = statusCode,
            title = GetTitle(statusCode),
            message = statusCode == (int)HttpStatusCode.InternalServerError
                ? "An unexpected error occurred."
                : exception.Message
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    public static string GetTitle(int statusCode) => statusCode switch
    {
        400 => "Bad Request",
        404 => "Not Found",
        500 => "Internal Server Error",
        403 => "Forbidden",
        _ => "An Error Occurred"
    };
}