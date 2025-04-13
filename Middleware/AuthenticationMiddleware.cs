using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class UserAuthMiddleware
{
    private readonly RequestDelegate _next;

    // Constructor assigns the next delegate in the pipeline
    public UserAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Main middleware logic to check for Authorization header
    public async Task Invoke(HttpContext context)
    {
        // If the Authorization header is missing, return 401
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized - Missing Authorization header");
            return;
        }

        // Proceed to the next middleware
        await _next(context);
    }
}
