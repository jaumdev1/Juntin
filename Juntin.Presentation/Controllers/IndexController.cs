using Juntin.Middlewares.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Juntin.Presentation.Controllers;

[Route("api/[controller]")]
[UseSessionMiddleware]
public class IndexController : ControllerBase
{
    [HttpGet]
    public ActionResult Index()
    {
        return Ok("online");
    }
}