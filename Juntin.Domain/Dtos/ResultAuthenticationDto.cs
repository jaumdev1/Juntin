namespace Domain.Dtos;

public record ResultAuthenticationDto
{
    public ResultAuthenticationDto(string sessionId)
    {
        SessionId = sessionId;
    }

    public string SessionId { get; init; }
}