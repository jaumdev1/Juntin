namespace Domain.Dtos.UserJutin;

public record CreateUserJutinDto
{
    public CreateUserJutinDto(Guid userId, UserRole role, Guid juntinPlayId)
    {
        UserId = userId;
        Role = role;
        JuntinPlayId = juntinPlayId;
    }
    public Guid UserId { get; init; }
    public UserRole Role { get; init; }
    public Guid JuntinPlayId { get; init; }
    
};