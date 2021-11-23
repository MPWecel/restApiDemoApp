using Microsoft.Extensions.Logging;
using RestApiDemo.Kernel.Interfaces;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RestApiDemo.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string from, string subject, string body)
        {
            var emailClient = new SmtpClient("localhost");
            var message = new MailMessage
            {

                From = new MailAddress(from),
                Subject = subject,
                Body = body
            };
            message.To.Add(new MailAddress(to));
            //await emailClient.SendMailAsync(message);

            string diagnosticMsg = String.Format(
                "Sending email from {0} to {1}. Email subject: {2}.\nEmail body:\n{3}",
                from,
                to,
                subject,
                body
            );
            System.Diagnostics.Debug.WriteLine(diagnosticMsg);


            _logger.LogWarning($"Sending email to {to} from {from} with subject {subject}.");
        }
    }
}
