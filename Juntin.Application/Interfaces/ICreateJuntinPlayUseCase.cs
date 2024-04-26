using Domain.Common;
using Domain.Dtos.JuntinPlay;

namespace Juntin.Application.Interfaces;

public interface ICreateJuntinPlayUseCase  : IUseCaseBase<JuntinPlayDto, BasicResult<Guid>>
{
    
}