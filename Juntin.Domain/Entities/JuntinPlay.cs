namespace Domain.Entities;

public class JuntinPlay : BaseEntity
{
    public required string Name { get; set; }

    public required string Category { get; set; }

    public required string Color { get; set; }
    
    public required string TextColor { get; set; }
    public Guid OwnerId { get; set; }

    public required User Owner { get; set; }
    
    public List<JuntinMovie> JuntinMovies { get; set; }
    public List<UserJuntin> UserJuntins { get; set; }
}