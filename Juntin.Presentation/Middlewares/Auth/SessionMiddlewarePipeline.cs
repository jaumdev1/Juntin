namespace Juntin.Middlewares.Auth;

public class SessionMiddlewarePipeline : IMiddleware
{
    private readonly MiddlewarePipeline _pipeline;

    public SessionMiddlewarePipeline(MiddlewarePipeline pipeline)
    {
        _pipeline = pipeline;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (_pipeline.Pipeline == null)
            await next(context);
        else
            await _pipeline.Pipeline(context);
    }
}