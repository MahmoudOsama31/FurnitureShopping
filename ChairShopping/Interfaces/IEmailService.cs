using ChairShopping.Services;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace ChairShopping.Interfaces
{
    public interface IEmailService
    {
       Task SendEmailAsync(string email, string address, string subject, string body);
    }
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(string email, string address, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress(email, address));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

			using var client = new SmtpClient();
			await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.Auto).ConfigureAwait(false);
			await client.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password).ConfigureAwait(false);
			await client.SendAsync(message).ConfigureAwait(false);
			await client.DisconnectAsync(true).ConfigureAwait(false);
		}

    }
}
