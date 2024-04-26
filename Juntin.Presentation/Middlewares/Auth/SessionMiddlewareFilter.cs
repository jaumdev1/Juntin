using Microsoft.AspNetCore.Mvc.Filters;

namespace Juntin.Middlewares.Auth;

public class SessionMiddlewareFilter : IAsyncActionFilter
{
    private readonly SessionMiddleware _sessionMiddleware;

    public SessionMiddlewareFilter(SessionMiddleware sessionMiddleware)
    {
        _sessionMiddleware = sessionMiddleware;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await _sessionMiddleware.Invoke(context.HttpContext, async () =>
        {
            if (context.HttpContext.Response.StatusCode != 401) await next();
        });
    }
}