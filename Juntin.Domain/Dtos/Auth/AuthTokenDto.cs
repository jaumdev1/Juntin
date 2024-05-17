namespace Domain.Dtos.Auth;

public record AuthTokenDto
{
    public AuthTokenDto(string authToken, string refreshToken)
    {
        AuthToken = authToken;
        RefreshToken = refreshToken;
    }
    
    public string AuthToken { get; set; }
    public string RefreshToken { get; set; }
};