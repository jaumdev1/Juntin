namespace Domain.Dtos.JuntinPlay;

public record JuntinPlayDto
{
    public JuntinPlayDto(string name, string category, string color, string textColor)
    {
        Name = name;
        Category = category;
        Color = color;
        TextColor = textColor;
    }

    public string Name { get; set; }
    public string Category { get; set; }
    public string Color { get; set; }
    public string TextColor { get; set; }
}