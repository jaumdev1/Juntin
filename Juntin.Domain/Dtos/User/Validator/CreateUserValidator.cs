using FluentValidation;

namespace Domain.Dtos.User.Validator;

public class CreateUserValidator : AbstractValidator<UserDto>
{
    public CreateUserValidator()
    {
        RuleFor(c => c.Username)
            .NotEmpty().WithMessage("UserName is mandatory")
            .MinimumLength(4).WithMessage("Size is smaller than allowed. Size allowed 4");
        RuleFor(c => c.Password).NotEmpty().WithMessage("Password is mandatory");
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email is mandatory");
        RuleFor(c => c.Email).EmailAddress().WithMessage("Email is invalid");
        RuleFor(c => c.ConfirmPassword).Equal(c => c.Password)
            .WithMessage("Password and ConfirmPassword are different");
    }
}