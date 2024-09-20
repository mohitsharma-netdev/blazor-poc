namespace BlazorServerAppDemo.Middleware;

/// <summary>
/// This custom middleware will be invoked for each request to the server.
/// </summary>
public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Custom logic before the next middleware
        
        // you can see this header in the network traffic tab in the browser
        context.Response.Headers.TryAdd("middleware-demo", "Hello from custom middleware!");

        await _next(context);

        // Custom logic after the next middleware
    }
}
