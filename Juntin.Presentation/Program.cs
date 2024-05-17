using Juntin.Application;
using Juntin.Infrastructure;
using Juntin.Infrastructure.Data;
using Juntin.Middlewares;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDataProtection();

var dataProtectionProvider = builder.Services.BuildServiceProvider().GetRequiredService<IDataProtectionProvider>();

var configurationString = builder.Configuration.GetSection("Redis:Configuration").Value;
builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
    ConnectionMultiplexer.Connect(configurationString));

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddTransient<GlobalExceptionHandler>();
builder.Services.AddScoped<SessionMiddleware>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();


if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseMiddleware<GlobalExceptionHandler>();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}


app.Run();