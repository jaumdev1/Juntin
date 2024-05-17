namespace Domain.Dtos.InviteJuntinPlay;

public record ResultInviteJuntinPlayDto
{
    public ResultInviteJuntinPlayDto(string code, string link)
    {
        Code = code;
        Link = link;
    }

    public string Code;
    public string Link;
}