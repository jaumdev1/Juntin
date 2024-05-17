using FluentValidation;

namespace Domain.Dtos.JuntinPlay.Validator;

public class GetOneJuntinPlayValidator : AbstractValidator<GetOneJuntinPlayDto>
{
    public GetOneJuntinPlayValidator()
    {
        RuleFor(c => c.JuntinPlayId)
            .NotEmpty().WithMessage("JuntinPlayId is mandatory");
    }
}