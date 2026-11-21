using System.Diagnostics;

namespace MVCS01.Middlewares;

public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestTimingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Stopwatch watch = Stopwatch.StartNew();
        await _next(context);
        watch.Stop();
        Console.WriteLine($"{context.Request.Path} ==> {watch.ElapsedMilliseconds} ms");
    }
}