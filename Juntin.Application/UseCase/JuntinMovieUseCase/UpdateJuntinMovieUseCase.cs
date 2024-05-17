using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.JuntinMovie;
using Domain.Dtos.JuntinMovie.Validator;
using Domain.Entities;
using Juntin.Application.Interfaces;
using Juntin.Application.Interfaces.JuntinMovie;
using Juntin.Application.Security;
using Mapster;

namespace Juntin.Application.UseCase.JuntinMovieUseCase;

public class UpdateJuntinMovieUseCase : IUpdateJuntinMovieUseCase
{
 private readonly UpdateJuntinMovieValidator _updateJuntinMovieValidator;
 private readonly IJuntinMovieRepository _juntinMovieRepository;
 private readonly SessionManager _sessionManager;
 private readonly IUserJuntinRepository _userJuntinRepository;
 public UpdateJuntinMovieUseCase(IJuntinMovieRepository juntinMovieRepository, SessionManager sessionManager,   IUserJuntinRepository userJuntinRepository)
 {
  _juntinMovieRepository = juntinMovieRepository;
  _updateJuntinMovieValidator = new UpdateJuntinMovieValidator();
  _sessionManager = sessionManager;
  _userJuntinRepository = userJuntinRepository;
 }
 public async Task<BasicResult> Execute(UpdateJuntinMovieDto input)
 {
  try
  {
   var validations = await _updateJuntinMovieValidator.ValidateAsync(input);

   if (!validations.IsValid) return DefaultValidator<BasicResult>.ReturnError(validations);
   var juntinMovieMapped = input.Adapt<JuntinMovie>();
   
   var ownerId = await _sessionManager.GetUserLoggedId();
   
   var juntin = await _juntinMovieRepository.GetById(input.Id);
   
   var isUserJuntin = await _userJuntinRepository.IsUserJuntin(juntin.JuntinPlayId, ownerId);
   
   if (!isUserJuntin)
    return BasicResult.Failure(new Error(HttpStatusCode.Forbidden,
     "You are not allowed to access this resource"));
   

   juntin.Description = input.Description;
   juntin.UrlImage = input.UrlImage;
   juntin.TmdbId = input.TmdbId;
   juntin.Title = input.Title;
   
  await _juntinMovieRepository.Update(juntin);
  
   return BasicResult.Success();
  }
  catch (Exception ex)
  {
   return BasicResult.Failure(new Error(HttpStatusCode.InternalServerError, ex.Message));
  }
 }
 
}