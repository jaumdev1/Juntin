using FluentValidation;

namespace Domain.Dtos.JuntinPlay.Validator;

public class DeleteJuntinPlayValidator : AbstractValidator<DeleteJuntinPlayDto>
{
    public DeleteJuntinPlayValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is mandatory");
    }
}