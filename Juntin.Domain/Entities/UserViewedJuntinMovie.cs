namespace Domain.Entities;

public class UserViewedJuntinMovie : BaseEntity

{

    public required Guid UserJuntinId { get; set; }
    public UserJuntin UserJuntin { get; set; }
    public bool IsViewed { get; set; }
    public  required Guid JuntinMovieId { get; set; }
    public JuntinMovie JuntinMovie { get; set; }
    public DateTime ViewedAt { get; set; }
    
}