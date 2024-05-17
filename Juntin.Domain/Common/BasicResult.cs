using System.Net;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Domain.Common;

public class BasicResult
{
    #region Constructors

    protected BasicResult(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    #endregion Constructors

    protected static BasicResult<TValue> Create<TValue>(TValue? value)
    {
        return value is not null ? Success(value) : Failure<TValue>(Error.None);
    }

    private static Error ReturnErrorMessage(HttpStatusCode statusCode, List<ValidationFailure> errors)
    {
        var errorMessages = errors.SelectMany(e => e.ErrorMessage!.Split('\n'))
            .Select(s => s.Trim())
            .ToList();

        return new Error(statusCode, JsonConvert.SerializeObject(errorMessages));
    }

    #region Properties

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    #endregion Properties

    #region Success

    public static BasicResult Success()
    {
        return new BasicResult(true, Error.None);
    }

    public static BasicResult<TValue> Success<TValue>(TValue value)
    {
        return new BasicResult<TValue>(value, true, Error.None);
    }

    #endregion Sucess

    #region Failure

    public static BasicResult Failure(Error error)
    {
        return new BasicResult(false, error);
    }

    public static BasicResult Failure(HttpStatusCode statusCode, List<ValidationFailure> errors)
    {
        return new BasicResult(false, ReturnErrorMessage(statusCode, errors));
    }

    public static BasicResult<TValue> Failure<TValue>(Error error)
    {
        return new BasicResult<TValue>(default, false, error);
    }

    public static BasicResult<TValue> Failure<TValue>(HttpStatusCode statusCode, List<ValidationFailure> errors)
    {
        return new BasicResult<TValue>(default, false, ReturnErrorMessage(statusCode, errors));
    }

    #endregion Failure
}