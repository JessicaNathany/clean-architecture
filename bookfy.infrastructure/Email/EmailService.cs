using bookfy.application.Abstractions.Email;

namespace bookfy.infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(domain.Users.Email recipient, string subject, string body)
        {
            return Task.CompletedTask;
        }
    }
}
