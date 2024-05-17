namespace Domain.Dtos.JuntinMovie;

public record DeleteJuntinMovieDto
{    
     public DeleteJuntinMovieDto(Guid id)
     {
         Id = id;
     }
    
     public Guid Id { get; set; }
};