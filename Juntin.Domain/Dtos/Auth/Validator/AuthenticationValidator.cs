
using FluentValidation;

namespace Domain.Dtos.Auth.Validator;

public class AuthenticationValidator : AbstractValidator<AuthenticationDto>
{
    public AuthenticationValidator()
    {
        RuleFor(c => c.Username)
            .NotEmpty().WithMessage("UserName is mandatory");
        RuleFor(c => c.Password).NotEmpty().WithMessage("Password is mandatory");
    }
}