using Domain.Dtos.JuntinMovie;
using FluentValidation;

namespace Domain.Dtos.Movie.Validator;

public class GetJuntinMovieValidator : AbstractValidator<GetJuntinMovieDto>
{
    public GetJuntinMovieValidator()
    {
        RuleFor(c => c.JuntinPlayId)
            .NotEmpty().WithMessage("JuntinPlayId is mandatory");
    }
}