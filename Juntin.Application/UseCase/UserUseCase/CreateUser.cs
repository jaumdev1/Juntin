using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos;
using Domain.Dtos.User;
using Domain.Dtos.User.Validator;
using Domain.Entities;
using Juntin.Application.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Juntin.Application.UseCase.UserUseCase;

public class CreateUser : ICreateUserUseCase
{
    private readonly CreateUserValidator _createUserValidator;
    private readonly IUserRepository _userRepository;

    public CreateUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        //change this, for the  dependency injector, we are using the CreateUserValidator wrong
        _createUserValidator = new CreateUserValidator();
    }

    public async Task<BasicResult> Execute(UserDto input)
    {
        try
        {
            var validations = await _createUserValidator.ValidateAsync(input);

            if (!validations.IsValid) return DefaultValidator<UserDto>.ReturnError(validations);
            
            var walletMapped = input.Adapt<User>();

            walletMapped.Id = Guid.NewGuid();

            walletMapped.Password = BCrypt.Net.BCrypt.HashPassword(input.Password);
            await _userRepository.Add(walletMapped);

            return BasicResult.Success();
        }
        catch (DbUpdateException ex) when (ex.InnerException is Npgsql.PostgresException pgEx && pgEx.SqlState == "23505")
        {
            return BasicResult.Failure<Error>(new Error(HttpStatusCode.BadRequest, "Email or username already exists"));
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<Error>(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}