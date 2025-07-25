using Microsoft.AspNetCore.Identity.UI.Services;

namespace Appointix.Services
{
    public class EmailService : IEmailSender
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Just logs the email to console/logs (for dev/testing)
            _logger.LogInformation($"Email to: {email}, Subject: {subject}, Body: {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}

