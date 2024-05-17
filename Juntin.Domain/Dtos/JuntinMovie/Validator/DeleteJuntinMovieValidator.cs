using FluentValidation;

namespace Domain.Dtos.JuntinMovie.Validator;

public class DeleteJuntinMovieValidator: AbstractValidator<DeleteJuntinMovieDto>
{
    public DeleteJuntinMovieValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is mandatory");
    }
}