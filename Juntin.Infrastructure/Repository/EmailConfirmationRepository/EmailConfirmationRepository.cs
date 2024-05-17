using Domain.Contracts.Repository;
using Domain.Dtos.JuntinMovie;
using Domain.Entities;
using Juntin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Juntin.Infrastructure.Repository.EmailConfirmationRepository;

public class EmailConfirmationRepository: BaseRepository<EmailConfirmation>, IEmailConfirmationRepository
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public EmailConfirmationRepository(ApplicationDbContext dbContext, IConfiguration configuration) : base(dbContext)
    {
        _context = dbContext;
        _configuration = configuration;
    }
}