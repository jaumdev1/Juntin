using FluentValidation;

namespace Domain.Dtos.UserJutin.Validator;

public class CreateUserJutinValidator  : AbstractValidator<CreateUserJutinDto>
{
    public CreateUserJutinValidator()
    {
        RuleFor(c => c.UserId).NotEmpty().WithMessage("UserId is mandatory");
        RuleFor(c => c.Role).NotEmpty().WithMessage("Role is mandatory");
        RuleFor(c => c.JuntinPlayId).NotEmpty().WithMessage("JuntinPlayId is mandatory");
    }
    
}