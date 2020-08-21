using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using WebApi.Helpers;
using System.Text.Json;

namespace WebApi.Services
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }

    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;

        public EmailService(IOptions<AppSettings> appSettings) => _appSettings = appSettings.Value;

        public void Send(string to, string subject, string html, string from = null)
        {
            var smtpSettingsJson = Environment.GetEnvironmentVariable("TIMER_SMTP_JSON");
            var smtpSettings = JsonSerializer.Deserialize<SmtpSettings>(smtpSettingsJson);

            // create message
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(smtpSettings.sender);
            email.From.Add(new MailboxAddress(smtpSettings.from, smtpSettings.sender));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };
            if (smtpSettings.cc != null)
                email.Cc.Add(MailboxAddress.Parse(smtpSettings.cc));

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(smtpSettings.host, int.Parse(smtpSettings.port), SecureSocketOptions.StartTls);
            if (!smtp.IsConnected)
            {
                throw new Exception("Connection to smtp server failed.");
            }
            smtp.Authenticate(smtpSettings.user, smtpSettings.password);
            if (!smtp.IsAuthenticated)
            {
                throw new Exception("smtp server authentication failed.");
            }
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}