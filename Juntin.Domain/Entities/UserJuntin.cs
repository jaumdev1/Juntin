using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class UserJuntin : BaseEntity
{
    [ForeignKey("User")] public Guid UserId { get; set; }

    public User User { get; set; }
    
    public UserRole Role { get; set; }
    [ForeignKey("JuntinPlay")] public Guid JuntinPlayId { get; set; }

    public JuntinPlay JuntinPlay { get; set; }
}