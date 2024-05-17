using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.JuntinPlay.Validator;
using Domain.Dtos.UserJutin;
using Domain.Dtos.UserJutin.Validator;
using Domain.Entities;
using Juntin.Application.Interfaces.UserJutin;
using Mapster;

namespace Juntin.Application.UseCase.UserJuntinUseCase;

public class CreateUserJuntin  : ICreateUserJutin
{ 
    
    private readonly IUserJuntinRepository _userJuntinRepository;
    private readonly CreateUserJutinValidator _createJuntinPlayValidator;
    public CreateUserJuntin(IUserJuntinRepository userJuntinRepository)
    {
        _userJuntinRepository = userJuntinRepository;
        _createJuntinPlayValidator = new CreateUserJutinValidator();
    }
  

    public async Task<BasicResult> Execute(CreateUserJutinDto input)
    {
        try
        {
            var validations = await _createJuntinPlayValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<CreateUserJutinDto>.ReturnError(validations);

            var userJutinMapped = input.Adapt<UserJuntin>();

            userJutinMapped.Id = Guid.NewGuid();
            
            await _userJuntinRepository.Add(userJutinMapped);
            return BasicResult.Success();
        }
        catch (Exception ex)
        {
            return BasicResult.Failure(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}