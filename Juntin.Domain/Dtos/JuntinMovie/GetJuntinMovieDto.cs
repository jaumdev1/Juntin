namespace Domain.Dtos.JuntinMovie;

public class GetJuntinMovieDto
{
    public  GetJuntinMovieDto()
    {
       
    }
    public GetJuntinMovieDto(Guid juntinPlayId, int page)
    {
        Page = page;
        JuntinPlayId = juntinPlayId;
    }

    public Guid JuntinPlayId { get; set; }
    public int Page { get; set; }
}