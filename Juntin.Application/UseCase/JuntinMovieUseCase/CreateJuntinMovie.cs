using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.JuntinMovie;
using Domain.Dtos.JuntinMovie.Validator;
using Domain.Entities;
using Juntin.Application.Interfaces.JuntinMovie;
using Juntin.Application.Security;
using Mapster;

namespace Juntin.Application.UseCase.JuntinMovieUseCase;

public class CreateJuntinMovie : ICreateJuntinMovieUseCase
{
    private readonly IJuntinMovieRepository _juntinPlayRepository;
    private readonly SessionManager _sessionManager;
    private readonly CreateJuntinMovieValidator _createJuntinMovieValidator;

    public CreateJuntinMovie(IJuntinMovieRepository juntinPlayRepository, SessionManager sessionManager) 
    {
        _juntinPlayRepository = juntinPlayRepository;
        _createJuntinMovieValidator = new CreateJuntinMovieValidator();
        _sessionManager = sessionManager;
    }

    public async Task<BasicResult<Guid>> Execute(JuntinMovieDto input)
    {
        try
        {
            var validations = await _createJuntinMovieValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<Guid>.ReturnError(validations);
            var juntinPlayMapped = input.Adapt<JuntinMovie>();
            
            juntinPlayMapped.Id = Guid.NewGuid();

            Guid ownerId =  await _sessionManager.GetUserLoggedId();
            
            juntinPlayMapped.UserId = ownerId;
            
            await _juntinPlayRepository.Add(juntinPlayMapped);
            
            return juntinPlayMapped.Id;
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<Guid>(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}