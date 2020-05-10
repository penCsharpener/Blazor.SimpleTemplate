using Blazor.SimpleTemplate.Models.Config;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Blazor.SimpleTemplate.Services {
    public class EmailSender : IEmailSender {
        private readonly AuthMessageSenderOptions _options;
        private readonly string _sender;

        public EmailSender(IOptions<SmtpConfiguration> options) {
            _options = options.Value.AuthMessageSenderOptions;
            _sender = options.Value.UserEmail;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage) {
            return Execute(_options.SendGridKey, subject, htmlMessage, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email) {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage() {
                From = new EmailAddress(_sender, _options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
