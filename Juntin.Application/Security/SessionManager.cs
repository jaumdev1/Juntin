using System.Security.Cryptography;
using System.Text;
using Domain.Contracts.Repository;
using Domain.Dtos.Auth;
using Mapster;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Juntin.Application.Security;

public class SessionManager
{
    private static ConnectionMultiplexer redis;
    private static IDatabase db;
    private static IDataProtector _protector;
    private static IConnectionMultiplexer _connectionMultiplexer;
    private static IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionManager(IConnectionMultiplexer connectionMultiplexer, IDataProtectionProvider provider,
        IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _protector = provider.CreateProtector("MySessionProtection");
        db = _connectionMultiplexer.GetDatabase();
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public string CreateSession(Guid userId, string username, TimeSpan expirationTime)
    {
        if (string.IsNullOrEmpty(username))
            throw new ArgumentException("Username cannot be null or empty", nameof(username));

        var expirationTicks = DateTime.UtcNow.Add(expirationTime);
        var sessionData = new UserInfo(userId, username, expirationTicks);

        var encryptedSessionData = _protector.Protect(JsonConvert.SerializeObject(sessionData));

        var sessionId = GenerateUniqueSessionId(encryptedSessionData);

        db.StringSet(sessionId, encryptedSessionData, expirationTime);

        return sessionId;
    }

    public async Task<Guid> GetUserLoggedId()
    {
        var sessionId = _httpContextAccessor.HttpContext.Request.Cookies["Authorization"];
        if (string.IsNullOrEmpty(sessionId))
        {
            sessionId = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        }

        string encryptedSessionData = db.StringGet(sessionId);
        var sessionData = UnprotectSessionData(encryptedSessionData);

        var userSession = JsonConvert.DeserializeObject<UserInfo>(sessionData);
        if (userSession == null) throw new Exception("User not found");

        return userSession.UserId;
    }

    private (string username, long expirationTicks) ParseSessionData(string sessionData)
    {
        var parts = sessionData.Split('|');
        var username = parts[0];
        var expirationTicks = long.Parse(parts[1]);

        return (username, expirationTicks);
    }


    public static UserInfo GetUserInfo(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
            throw new ArgumentException("Session ID cannot be null or empty", nameof(sessionId));

        string user = db.StringGet(sessionId);
        if (user == null) return null;

        var unprotectSessionData = UnprotectSessionData(db.StringGet(sessionId));

        var userSession = unprotectSessionData.Adapt<UserInfo>();

        return userSession;
    }

    public static void RemoveSession(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
            throw new ArgumentException("Session ID cannot be null or empty", nameof(sessionId));

        db.KeyDelete(sessionId);
    }
    
    public async Task<AuthTokenDto> RefreshToken()
    {
        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["RefreshAuthorization"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            refreshToken = _httpContextAccessor.HttpContext.Request.Headers["RefreshAuthorization"];
        }

        var userId = db.StringGet(refreshToken);
        var user = await _userRepository.GetById(Guid.Parse(userId));
        if (user == null) throw new Exception("User not found");
        
        
        var newAccessToken = CreateSession(user.Id, user.Username, TimeSpan.FromMinutes(1));
        var newRefreshToken = GenerateRefreshToken();
        db.KeyDelete(refreshToken);
        SaveRefreshToken(user.Id, newRefreshToken);
        
        var auth = new AuthTokenDto(newAccessToken, newRefreshToken);
        return auth;
    }
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    public void SaveRefreshToken(Guid userId, string refreshToken)
    {
        var expiryDate = DateTime.UtcNow.AddDays(7);
        db.StringSet(refreshToken, userId.ToString(), expiryDate - DateTime.UtcNow);
    }
    public static string UnprotectSessionData(string encryptedSessionData)
    {
        var sessionData = _protector.Unprotect(encryptedSessionData);

        return sessionData;
    }

    private string GenerateUniqueSessionId(string sessionData)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(sessionData));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}

public class UserInfo
{
    public UserInfo(Guid userId, string username, DateTime expirationTime)
    {
        if (string.IsNullOrEmpty(username))
            throw new ArgumentException("Username cannot be null or empty", nameof(username));

        Username = username;
        UserId = userId;
        ExpirationTime = expirationTime;
    }

    public Guid UserId { get; }
    public DateTime ExpirationTime { get; }
    public string Username { get; }
}