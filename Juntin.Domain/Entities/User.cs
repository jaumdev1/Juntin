namespace Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public bool ConfirmedEmail { get; set; }
    
    public UserRole Role { get; set; }

    public string? GoogleAccountId { get; set; }
    public string? GoogleAuthToken { get; set; }
}