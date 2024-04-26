namespace Domain.Dtos.JuntinPlay;

public record JuntinPlayResult
{
    public JuntinPlayResult(Guid id, string name, string category, string color) 
    {
        Id = id;
        Name = name;
        Category = category;
        Color = color;
      
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Color { get; set; }
 
};