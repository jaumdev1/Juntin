using Domain.Common;
using Domain.Dtos.JuntinPlay;

namespace Juntin.Application.Interfaces;

public interface IGetJuntinPlayUseCase : IUseCaseBase<GetJuntinPlayDto, BasicResult<List<JuntinPlayResult>>>
{
}