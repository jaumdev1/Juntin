namespace Domain.Dtos.JuntinPlay;

public record JuntinPlayResult
{
    public JuntinPlayResult(Guid id, string name, string category, string color, string textColor,int peopleCount, int moviesCount)
    {
        Id = id;
        Name = name;
        Category = category;
        Color = color;
        TextColor = textColor;
        PeopleCount = peopleCount;
        MoviesCount = moviesCount;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Color { get; set; }
    public string TextColor { get; set; }
    
    public int PeopleCount { get; set; }
    public int MoviesCount { get; set; }
    
}