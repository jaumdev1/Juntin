using Domain.Contracts.Repository;
using Juntin.Infrastructure.Data;
using Juntin.Infrastructure.Repository.EmailConfirmationRepository;
using Juntin.Infrastructure.Repository.InviteJuntinPlayRepository;
using Juntin.Infrastructure.Repository.JuntinMovieRepository;
using Juntin.Infrastructure.Repository.JuntinRepository;
using Juntin.Infrastructure.Repository.UserJuntinRepository;
using Juntin.Infrastructure.Repository.UserRepository;
using Juntin.Infrastructure.Repository.UserViewedJuntinMovieRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Juntin.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJuntinPlayRepository, JuntinPlayRepository>();
        services.AddScoped<IUserJuntinRepository, UserJuntinRepository>();
        services.AddScoped<IJuntinMovieRepository, JuntinMovieRepository>();
        services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
        services.AddScoped<IUserViewedJuntinMovieRepository, UserViewedJuntinMovieRepository>();
        services.AddScoped<IInviteJuntinPlayRepository, InviteJuntinPlayRepository>();
        return services;
    }
}