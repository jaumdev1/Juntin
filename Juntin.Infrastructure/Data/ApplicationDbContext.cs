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

    public DbSet<User> User { get; set; }
    public DbSet<AdminJuntin> AdminJuntin { get; set; }
    public DbSet<JuntinMovie> JuntinMovie { get; set; }
    public DbSet<JuntinPlay> JuntinPlay { get; set; }
    public DbSet<UserJuntin> UserJuntin { get; set; }
    public DbSet<EmailConfirmation> EmailConfirmation { get; set; }
    public DbSet<UserViewedJuntinMovie> UserViewedJuntinMovie { get; set; }
    public DbSet<InviteJuntinPlay> InviteJuntinPlay { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                builder.Entity(entityType.ClrType).HasQueryFilter(GetIsDeletedRestriction(entityType.ClrType));
               
            }
        }
        
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

    public async Task<InviteJuntinPlay> FirstOrDefaultAsync(Func<object, bool> func)
    {
        throw new NotImplementedException();
    }
}