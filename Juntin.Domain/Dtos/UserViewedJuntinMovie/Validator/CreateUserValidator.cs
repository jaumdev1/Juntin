using Domain.Dtos.UserViewedJuntinMovie;
using FluentValidation;

namespace Domain.Dtos.User.Validator;

public class CreateUserViewedJuntinMovieValidator : AbstractValidator<CreateUserViewedJuntinMovieDto>
{
    public CreateUserViewedJuntinMovieValidator()
    {
        RuleFor(c => c.JuntinMovieId)
            .NotEmpty().WithMessage("Juntin Movie  is mandatory");
     
    }
}