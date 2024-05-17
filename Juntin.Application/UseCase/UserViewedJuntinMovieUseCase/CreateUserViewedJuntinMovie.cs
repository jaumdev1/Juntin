using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.JuntinMovie.Validator;
using Domain.Dtos.User;
using Domain.Dtos.User.Validator;
using Domain.Dtos.UserViewedJuntinMovie;
using Domain.Entities;
using Juntin.Application.Interfaces.UserViewedJuntinMovie;
using Juntin.Application.Security;

namespace Juntin.Application.UseCase.UserViewedJuntinMovieUseCase;

public class CreateUserViewedJuntinMovie : ICreateUserViewedJuntinMovie
{
    private readonly IUserViewedJuntinMovieRepository _userViewedJuntinMovieRepository;
    private CreateUserViewedJuntinMovieValidator _createUserValidator;
    private readonly SessionManager _sessionManager;
    private readonly IUserJuntinRepository _userJuntinRepository;
    private readonly IJuntinMovieRepository _juntinMovieRepository;
    private readonly IJuntinPlayRepository _juntinPlayRepository;
    public CreateUserViewedJuntinMovie( IUserViewedJuntinMovieRepository userViewedJuntinMovieRepository, 
        SessionManager sessionManager,    
        IUserJuntinRepository userJuntinRepository, 
        IJuntinMovieRepository juntinMovieRepository, IJuntinPlayRepository juntinPlayRepository)
    {
        _userViewedJuntinMovieRepository = userViewedJuntinMovieRepository;
        _createUserValidator = new CreateUserViewedJuntinMovieValidator();
        _sessionManager = sessionManager;
        _userJuntinRepository = userJuntinRepository;
        _juntinMovieRepository = juntinMovieRepository;
        _juntinPlayRepository = juntinPlayRepository;
    }
    public async Task<BasicResult<ResultViwedJuntinMovieDto>> Execute(CreateUserViewedJuntinMovieDto input)
    {
        try
        {
            var validations = await _createUserValidator.ValidateAsync(input);
            if (!validations.IsValid) 
                return DefaultValidator<ResultViwedJuntinMovieDto>.ReturnError(validations);

            var ownerId = await _sessionManager.GetUserLoggedId();
            var juntinMovie = await _juntinMovieRepository.GetById(input.JuntinMovieId);

            if (juntinMovie == null || juntinMovie.JuntinPlayId == null)
                return BasicResult.Failure<ResultViwedJuntinMovieDto>(new Error(HttpStatusCode.NotFound, "Movie not found"));
            if(juntinMovie.IsWatchedEveryone)
                return BasicResult.Failure<ResultViwedJuntinMovieDto>(new Error(HttpStatusCode.Forbidden, "This movie has already been watched by everyone"));
            
            var juntinplay = await _juntinPlayRepository.GetByIdAndListUserJuntin(juntinMovie.JuntinPlayId); 
            
            if (juntinplay == null)
                return BasicResult.Failure<ResultViwedJuntinMovieDto>(new Error(HttpStatusCode.NotFound, "JuntinPlay not found"));
            
            var isUserJuntin = await _userJuntinRepository.IsUserJuntin(juntinMovie.JuntinPlayId, ownerId);
            if (!isUserJuntin)
                return BasicResult.Failure<ResultViwedJuntinMovieDto>(new Error(HttpStatusCode.Forbidden, "You are not allowed to access this resource"));
            
            var userJuntin = await _userJuntinRepository.GetByJuntinPlayAndUser(juntinMovie.JuntinPlayId, ownerId);
            
            var existingUserViewedJuntinMovie = await _userViewedJuntinMovieRepository.GetByJuntinUserAndJuntinMovie(userJuntin.Id, input.JuntinMovieId);
            
            
            if(input.IsViewed)
                juntinMovie.Views++;
            else 
                juntinMovie.Views--;
        
             if(juntinMovie.Views >= juntinplay.UserJuntins.Count)
                juntinMovie.IsWatchedEveryone = true;
          
            await _juntinMovieRepository.Update(juntinMovie);
         
            var isNewEntry = false;

            if (existingUserViewedJuntinMovie == null)
            {
                existingUserViewedJuntinMovie = new UserViewedJuntinMovie
                {
                    Id = Guid.NewGuid(),
                    UserJuntinId = userJuntin.Id,
                    JuntinMovieId = input.JuntinMovieId,
                    ViewedAt = DateTime.UtcNow
                };
                isNewEntry = true;
            }
            
            existingUserViewedJuntinMovie.IsViewed = input.IsViewed;
            existingUserViewedJuntinMovie.ViewedAt = DateTime.UtcNow;
            
            
            if (isNewEntry)
                 await _userViewedJuntinMovieRepository.Add(existingUserViewedJuntinMovie);
            else
                await _userViewedJuntinMovieRepository.Update(existingUserViewedJuntinMovie);

            var result = new ResultViwedJuntinMovieDto(juntinMovie.IsWatchedEveryone);
            
            return BasicResult.Success(result);
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<ResultViwedJuntinMovieDto>(new Error(HttpStatusCode.InternalServerError, "Error to view movie"));
        }
    }

}