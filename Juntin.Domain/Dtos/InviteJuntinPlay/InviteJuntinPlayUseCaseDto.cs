namespace Domain.Dtos.InviteJuntinPlay;

public record InviteJuntinPlayUseCaseDto
{
    public InviteJuntinPlayUseCaseDto(string code, bool isAccepted)
    {
        Code = code;
        IsAccepted = isAccepted;
    }
    public string Code { get; set; }    
    public bool IsAccepted { get; set; }
};