using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.User;
using Domain.Dtos.User.Validator;
using Domain.Entities;
using Juntin.Application.Interfaces;
using Juntin.Application.Interfaces.EmailConfirmationContract;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Juntin.Application.UseCase.UserUseCase;

public class CreateUser : ICreateUserUseCase
{
    private readonly CreateUserValidator _createUserValidator;
    private readonly IUserRepository _userRepository;
    private readonly IEmailConfirmation _emailConfirmation;
    public CreateUser(IUserRepository userRepository, IEmailConfirmation emailConfirmation)
    {
        _userRepository = userRepository;
        _emailConfirmation = emailConfirmation;
        
        _createUserValidator = new CreateUserValidator();
    }
  

    public async Task<BasicResult> Execute(UserDto input)
    {
        try
        {
            var validations = await _createUserValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<UserDto>.ReturnError(validations);

            var userMapped = input.Adapt<User>();

            userMapped.Id = Guid.NewGuid();
            
            userMapped.Password = BCrypt.Net.BCrypt.HashPassword(input.Password);
          
            await _userRepository.Add(userMapped);
            // await _emailConfirmation.Execute(userMapped);
            
            return BasicResult.Success();
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
        {
            return BasicResult.Failure<Error>(new Error(HttpStatusCode.BadRequest, "Email or username already exists"));
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<Error>(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}