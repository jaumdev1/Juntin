namespace Juntin.Middlewares.Auth;

public class MiddlewarePipeline
{
    public RequestDelegate Pipeline { get; private set; }

    public MiddlewarePipeline Use(Func<RequestDelegate, RequestDelegate> middleware)
    {
        Pipeline = middleware(Pipeline);
        return this;
    }
}