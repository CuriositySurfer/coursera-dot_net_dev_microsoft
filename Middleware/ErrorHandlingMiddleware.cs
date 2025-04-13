using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    // Constructor receives the next delegate in the pipeline
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Middleware logic to catch unhandled exceptions and return a standardized response
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Set response details for unhandled exceptions
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new { error = "Internal server error." };
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
