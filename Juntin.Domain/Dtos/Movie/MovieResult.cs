namespace Domain.Dtos.Movie;

public record MovieResult
{
    
    private string _urlImage = "https://image.tmdb.org/t/p/w500";

    public MovieResult(string title, string pathImg, string description, int tmdbId)
    {
        Title = title;
        Description = description;
        TmdbId = tmdbId;
        UrlImage = _urlImage + pathImg;
    }

    public string Title { get; set; }

    public string UrlImage
    {
        get => _urlImage;
        set => _urlImage = $"{value}";
    }

    public string Description { get; set; }
    public int TmdbId { get; set; }
}