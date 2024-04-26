using Juntin.Application.Interfaces;
using Juntin.Application.Security;
using Juntin.Application.UseCase.AuthenticationUseCase;
using Juntin.Application.UseCase.JuntinPlayUseCase;
using Juntin.Application.UseCase.UserUseCase;
using Microsoft.AspNetCore.Http;
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
        services.AddScoped<SessionManager>();
       
        return services;
    }
}