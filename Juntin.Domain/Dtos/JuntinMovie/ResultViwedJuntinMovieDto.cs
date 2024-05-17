namespace Domain.Dtos.JuntinMovie.Validator;

public record ResultViwedJuntinMovieDto
{
    public ResultViwedJuntinMovieDto(bool isWatchedEveryone)
    {
        IsWatchedEveryone = isWatchedEveryone;
    }
    public bool IsWatchedEveryone { get; set; }
}