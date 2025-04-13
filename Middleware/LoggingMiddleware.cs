using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    // Constructor assigns the next delegate in the pipeline
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Logs basic request and response info
    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine($"[Request] {context.Request.Method} {context.Request.Path}");

        await _next(context);

        Console.WriteLine($"[Response] Status Code: {context.Response.StatusCode}");
    }
}
