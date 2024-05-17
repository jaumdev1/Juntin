using FluentValidation;

namespace Domain.Dtos.JuntinPlay.Validator;

public class UpdateJuntinPlayValidator : AbstractValidator<UpdateJuntinPlayDto>
{
    public UpdateJuntinPlayValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is mandatory");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is mandatory")
            .MinimumLength(4).WithMessage("Size is smaller than allowed. Size allowed 4");
        RuleFor(c => c.Category).NotEmpty().WithMessage("Category is mandatory");
        RuleFor(c => c.Color).NotEmpty().WithMessage("Color is mandatory");
    }
}