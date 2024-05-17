using FluentValidation;

namespace Domain.Dtos.JuntinMovie.Validator;

public class UpdateJuntinMovieValidator : AbstractValidator<UpdateJuntinMovieDto>
{
    public UpdateJuntinMovieValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is mandatory");
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Title is mandatory")
            .MinimumLength(4).WithMessage("Size is smaller than allowed. Size allowed 4");
        RuleFor(c => c.Description) 
            .NotEmpty().WithMessage("Description is mandatory")
            .MinimumLength(4).WithMessage("Size is smaller than allowed. Size allowed 4");
        RuleFor(c => c.UrlImage).NotEmpty().WithMessage("Director is mandatory");

        RuleFor(c => c.TmdbId).NotEmpty().WithMessage("Tmdb Identificator is mandatory");
        
    }
    
}