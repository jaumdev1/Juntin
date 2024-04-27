using FluentValidation;

namespace Domain.Dtos.JuntinMovie.Validator;

public class CreateJuntinMovieValidator : AbstractValidator<JuntinMovieDto>
{
    public CreateJuntinMovieValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Title is mandatory")
            .MinimumLength(4).WithMessage("Size is smaller than allowed. Size allowed 4");
        
        RuleFor(c => c.UrlImage).NotEmpty().WithMessage("Director is mandatory");
        
        RuleFor(c => c.TmdbId).NotEmpty().WithMessage("Tmdb Identificator is mandatory");
        
        RuleFor(c => c.JuntinPlayId).NotEmpty().WithMessage("JuntinPlay Identificator is mandatory");
        
     
        
    }
}