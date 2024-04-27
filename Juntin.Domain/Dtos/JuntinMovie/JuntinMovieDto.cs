namespace Domain.Dtos.JuntinMovie;


public record JuntinMovieDto
{
    public JuntinMovieDto() 
    {

      
    }
    public string Title { get; set; }
    public string UrlImage { get; set; }
    public string Description { get; set; }
    public int TmdbId { get; set; }
    public Guid UserId { get; set; }
    public Guid JuntinPlayId { get; set; }

 
};