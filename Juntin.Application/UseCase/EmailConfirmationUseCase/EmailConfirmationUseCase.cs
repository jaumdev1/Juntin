using System.Net;
using Domain.Common;
using Domain.Contracts.Repository;
using Domain.Dtos.User;
using Domain.Entities;
using Juntin.Application.Interfaces.Email;
using Juntin.Application.Interfaces.EmailConfirmationContract;

namespace Juntin.Application.UseCase.EmailConfirmationUseCase;

public class EmailConfirmationUseCase : IEmailConfirmation
{
    private readonly IEmailConfirmationRepository _emailConfirmationRepository;
    private readonly ISendEmail _sendEmail;
    public EmailConfirmationUseCase(IEmailConfirmationRepository emailConfirmationRepository, ISendEmail email)
    {
        _emailConfirmationRepository = emailConfirmationRepository;
        _sendEmail = email;
    }

    public async Task<BasicResult<bool>> Execute(User input)
    {
       
           
            var emailConfirmationToken = Guid.NewGuid().ToString();
            var emailConfirmationTokenEncripted = BCrypt.Net.BCrypt.HashPassword(emailConfirmationToken);  
            var emailconfirmation = new EmailConfirmation()
            {
                UserId = input.Id,
                ConfirmationToken = emailConfirmationTokenEncripted,
           
            };

            await _emailConfirmationRepository.Add(emailconfirmation);
        
            var confirmationLink = $"http://localhost:5193.com/confirm-email?token={emailConfirmationToken}";
            await _sendEmail.Execute(input.Email, "Confirm your email", $"Please confirm your email by clicking on the link: {confirmationLink}");

            return BasicResult<bool>.Success(true);
        }
       
    
}