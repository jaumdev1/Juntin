namespace Juntin.Application.Interfaces.Email;

public interface ISendEmail
{
    Task Execute(string email, string subject, string message);
}