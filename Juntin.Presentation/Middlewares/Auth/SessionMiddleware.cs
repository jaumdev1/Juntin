using Juntin.Application.Security;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Juntin.Middlewares;

public class SessionMiddleware
{
    private readonly IDatabase _db;

    public SessionMiddleware(IConnectionMultiplexer connectionMultiplexer)
    {
        _db = connectionMultiplexer.GetDatabase();
    }

    public async Task Invoke(HttpContext context, Func<Task> next)
    {
        var sessionId = context.Request.Cookies["Authorization"];

       
        if (string.IsNullOrEmpty(sessionId))
        {
            sessionId = context.Request.Headers.Authorization;
        }

        if (!string.IsNullOrEmpty(sessionId))
        {
            string encryptedSessionData = _db.StringGet(sessionId);

            if (!string.IsNullOrEmpty(encryptedSessionData))
                try
                {
                    var sessionDataUnprotect = SessionManager.UnprotectSessionData(encryptedSessionData);
                    var userSession = JsonConvert.DeserializeObject<UserInfo>(sessionDataUnprotect);

                    if (userSession.ExpirationTime > DateTime.UtcNow)
                    {
                        await next();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error decrypting session data: " + ex.Message);
                }
        }

        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
    }

    private object UnprotectSessionData(string sessionDataUnprotect)
    {
        throw new NotImplementedException();
    }
}