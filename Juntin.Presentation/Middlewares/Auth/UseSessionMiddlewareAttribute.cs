using Microsoft.AspNetCore.Mvc.Filters;

namespace Juntin.Middlewares.Auth;

public class UseSessionMiddlewareAttribute : Attribute, IFilterFactory
{
    public bool IsReusable => false;

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        var sessionMiddleware = serviceProvider.GetRequiredService<SessionMiddleware>();
        return new SessionMiddlewareFilter(sessionMiddleware);
    }
}