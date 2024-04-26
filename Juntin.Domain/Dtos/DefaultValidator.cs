using System.Net;
using Domain.Common;
using FluentValidation.Results;

namespace Domain.Dtos;

public static class DefaultValidator<T>
{
    public static void ValidateDto(ValidationResult validations)
    {
        if (!validations.IsValid) ReturnError(validations);
    }

    public static BasicResult<T> ReturnError(ValidationResult validations)
    {
        return BasicResult.Failure<T>(HttpStatusCode.BadRequest, validations.Errors);
    }
}