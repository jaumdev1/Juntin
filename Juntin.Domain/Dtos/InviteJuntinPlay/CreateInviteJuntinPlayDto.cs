namespace Domain.Dtos.InviteJuntinPlay;

public record CreateInviteJuntinPlayDto
{
    public CreateInviteJuntinPlayDto(Guid juntinPlayId)
    {
    JuntinPlayId = juntinPlayId;

    }
    public Guid JuntinPlayId { get; set; }
    
};