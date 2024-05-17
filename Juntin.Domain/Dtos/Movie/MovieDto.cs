namespace Domain.Dtos.Movie;

public record MovieDto
{
    public MovieDto()
    {
    }
    public MovieDto(string title)
    {
        Title = title;
    }

    public string Title { get; set; }
}