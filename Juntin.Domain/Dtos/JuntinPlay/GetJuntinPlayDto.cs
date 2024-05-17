namespace Domain.Dtos.JuntinPlay;

public class GetJuntinPlayDto
{
    public GetJuntinPlayDto()
    {
    }

    public GetJuntinPlayDto(int page)
    {
        Page = page;
    }
    public int Page { get; set; }
}