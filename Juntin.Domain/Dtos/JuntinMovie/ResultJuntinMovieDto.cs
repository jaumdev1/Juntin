namespace Domain.Dtos.JuntinMovie;

public record ResultJuntinMovieDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string UrlImage { get; set; }
    public string Description { get; set; }
    
    public bool IsViewedToUser { get; set; }
    public int TmdbId { get; set; }
    public string UserName { get; set; }
    public Guid JuntinPlayId { get; set; }
}