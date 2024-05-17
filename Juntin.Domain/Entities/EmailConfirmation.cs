using Domain.Common;

namespace Domain.Entities;


public class EmailConfirmation : BaseEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ConfirmationToken { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public User User { get; set; }
}