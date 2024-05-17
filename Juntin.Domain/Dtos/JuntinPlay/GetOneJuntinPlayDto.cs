namespace Domain.Dtos.JuntinPlay.Validator;

public record GetOneJuntinPlayDto
{
    public GetOneJuntinPlayDto(Guid juntinPlayId)
    {
        JuntinPlayId = juntinPlayId;
    }
    public Guid JuntinPlayId { get; set; }
}