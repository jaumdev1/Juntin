using Domain.Dtos.JuntinPlay.Validator;
using Domain.Entities;
using Juntin.Application.Interfaces;
using Juntin.Application.Interfaces.Email;
using Juntin.Application.Interfaces.EmailConfirmationContract;
using Juntin.Application.Interfaces.InviteJuntinPLay;
using Juntin.Application.Interfaces.JuntinMovie;
using Juntin.Application.Interfaces.Movie;
using Juntin.Application.Interfaces.UserJutin;
using Juntin.Application.Interfaces.UserViewedJuntinMovie;
using Juntin.Application.Security;
using Juntin.Application.UseCase.AuthenticationUseCase;
using Juntin.Application.UseCase.EmailConfirmationUseCase;
using Juntin.Application.UseCase.InviteUseCase;
using Juntin.Application.UseCase.JuntinMovieUseCase;
using Juntin.Application.UseCase.JuntinPlayUseCase;
using Juntin.Application.UseCase.MovieUseCase;
using Juntin.Application.UseCase.UserJuntinUseCase;
using Juntin.Application.UseCase.UserUseCase;
using Juntin.Application.UseCase.UserViewedJuntinMovieUseCase;
using Microsoft.Extensions.DependencyInjection;

namespace Juntin.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateUserUseCase, CreateUser>();
        services.AddScoped<ICreateAuthenticationUseCase, CreateAuthentication>();
        services.AddScoped<ICreateJuntinPlayUseCase, CreateJuntinPlay>();
        services.AddScoped<IUpdateJuntinPlayUseCase, UpdateJuntinPlay>();
        services.AddScoped<IDeleteJuntinPlayUseCase, DeleteJuntinPlay>();
        services.AddScoped<IGetJuntinPlayUseCase, GetJuntinPlay>();
        services.AddScoped<IGetJuntinMovieUseCase, GetJuntinMovie>();
        services.AddScoped<ICreateJuntinMovieUseCase, CreateJuntinMovie>();
        services.AddScoped<IUpdateJuntinMovieUseCase, UpdateJuntinMovieUseCase>();
        services.AddScoped<IDeleteJuntinMovieUseCase, DeleteJuntinMovie>();
        services.AddScoped<IMovieUseCase, Movie>();
        services.AddScoped<IRefreshAuthenticationUseCase, RefreshAuthentication>();
        services.AddScoped<ISendEmail, SendEmail>();
        services.AddScoped<IEmailConfirmation, EmailConfirmationUseCase>();
        services.AddScoped<SessionManager>();
        services.AddScoped<ICreateUserViewedJuntinMovie, CreateUserViewedJuntinMovie>();
        services.AddScoped<ICreateUserJutin, CreateUserJuntin>();
        services.AddScoped<IGetHistoricJuntinMovie, GetHistoricJuntinMovie>();
        services.AddScoped<IGetOneJuntinPlayUseCase, GetOneJuntinPlay>();
        services.AddScoped<ICreateInviteJuntinPlayUseCase, CreateInvite>();
        services.AddScoped<IInviteJuntinPlayUseCase, InviteJuntinPlayUseCase>();
        
        return services;
    }
}