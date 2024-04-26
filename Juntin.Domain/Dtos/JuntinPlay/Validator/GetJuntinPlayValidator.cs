using FluentValidation;

namespace Domain.Dtos.JuntinPlay.Validator;

public class GetJuntinPlayValidator: AbstractValidator<GetJuntinPlayDto>
{
    public GetJuntinPlayValidator()
    {
        RuleFor(c => c.Page)
            .NotEmpty().WithMessage("Page is mandatory");
    }
}