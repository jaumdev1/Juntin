using Microsoft.AspNetCore.Mvc;

namespace Juntin.Middlewares.Auth;

public class MiddlewareFilterAttribute : TypeFilterAttribute
{
    public MiddlewareFilterAttribute(Type filterType) : base(filterType)
    {
    }
}