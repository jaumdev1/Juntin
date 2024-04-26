namespace Domain.Dtos.JuntinPlay;

public record JuntinPlayDto
{
    public JuntinPlayDto(string name, string category, string color) 
    {
        Name = name;
        Category = category;
        Color = color;
      
    }
  
    public string Name { get; set; }
    public string Category { get; set; }
    public string Color { get; set; }
 
};