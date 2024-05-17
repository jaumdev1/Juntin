namespace Domain.Entities;

public class InviteJuntinPlay : BaseEntity
{
    public string Code { get; set; }
    
    public Guid JuntinPlayId { get; set; }
    
    public JuntinPlay JuntinPlay { get; set; }
    
    public DateTime ExpireAt { get; set; }
    
    public string Link { get; set; }
}