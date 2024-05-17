using Domain.Common;
using Domain.Dtos.JuntinPlay;
using Domain.Dtos.JuntinPlay.Validator;

namespace Juntin.Application.Interfaces;

public interface IGetOneJuntinPlayUseCase : IUseCaseBase<GetOneJuntinPlayDto, BasicResult<JuntinPlayResult>>
{
}