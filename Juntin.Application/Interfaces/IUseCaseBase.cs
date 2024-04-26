namespace Juntin.Application.Interfaces;

public interface IUseCaseBase<TInput, TOutput>
{
    Task<TOutput> Execute(TInput input);
}