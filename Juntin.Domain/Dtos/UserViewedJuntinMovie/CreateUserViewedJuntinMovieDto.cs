namespace Domain.Dtos.UserViewedJuntinMovie;

public record CreateUserViewedJuntinMovieDto
{
    public CreateUserViewedJuntinMovieDto(Guid juntinMovieId, bool isViewed)
    {

        JuntinMovieId = juntinMovieId;
        IsViewed = isViewed;
    }
    public bool IsViewed { get; set; }
    public Guid JuntinMovieId { get; set; }
    
};