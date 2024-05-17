using FluentValidation;

namespace Domain.Dtos.InviteJuntinPlay.Validator;

public class CreateInviteJuntinPlayDtoValidator: AbstractValidator<CreateInviteJuntinPlayDto>
{
    public CreateInviteJuntinPlayDtoValidator()
    {
        RuleFor(c => c.JuntinPlayId).NotEmpty().WithMessage("JuntinPlay Identificator is mandatory");
    }
}