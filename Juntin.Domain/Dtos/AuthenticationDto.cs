namespace Domain.Dtos;

public record AuthenticationDto
{
    public AuthenticationDto(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; init; }
    public string Password { get; init; }
}