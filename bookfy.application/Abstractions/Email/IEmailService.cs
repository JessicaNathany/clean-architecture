namespace bookfy.application.Abstractions.Email
{
    public interface IEmailService
    {
        Task SendAsync(domain.Users.Email recipient, string subject, string body);
    }
}
