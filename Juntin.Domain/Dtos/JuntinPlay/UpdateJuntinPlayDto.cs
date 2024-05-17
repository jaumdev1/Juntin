namespace Domain.Dtos.JuntinPlay;

public record UpdateJuntinPlayDto
{
    public UpdateJuntinPlayDto(Guid id, string name, string category, string color, string textColor)
    {
        Id = id;
        Name = name;
        Category = category;
        Color = color;
        TextColor = textColor;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Color { get; set; }
    public string TextColor { get; set; }
}