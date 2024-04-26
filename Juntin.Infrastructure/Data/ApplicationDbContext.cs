using System.Linq.Expressions;
using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Juntin.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<AdminJuntin> AdminsJuntins { get; set; }
    public DbSet<JuntinMovie> JuntinsMovies { get; set; }
    public DbSet<JuntinPlay> JuntinsPlays { get; set; }
    public DbSet<UserJuntin> UsersJuntins { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in builder.Model.GetEntityTypes())
            if (entityType.ClrType.GetProperty("IsDeleted") != null)
                builder.Entity(entityType.Name).HasQueryFilter(GetIsDeletedRestriction(entityType.ClrType));
        builder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        builder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        
        base.OnModelCreating(builder);
    }

    private static LambdaExpression GetIsDeletedRestriction(Type type)
    {
        var param = Expression.Parameter(type, "t");
        var body = Expression.Equal(Expression.Property(param, "IsDeleted"), Expression.Constant(false));
        return Expression.Lambda(body, param);
    }
}