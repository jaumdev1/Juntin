namespace Domain.Dtos.JuntinMovie;

public record UpdateJuntinMovieDto
{
    public UpdateJuntinMovieDto(Guid id, string title, string description,string urlImage, int tmdbId)
    {
        Id = id;
        Title = title;
        Description = description;
        UrlImage = urlImage;
        TmdbId = tmdbId;
    }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string UrlImage { get; set; }
    public string Description { get; set; }
    public int TmdbId { get; set; }
};