using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UniTutor.Interface;

namespace UniTutor.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public EmailService(string smtpServer, int port, string username, string password)
        {
            _smtpServer = smtpServer;
            _port = port;
            _username = username;
            _password = password;
        }

        public async Task SendEmailAsync(string recipient, string subject, string body)
        {
            using (var smtpClient = new SmtpClient(_smtpServer, _port))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_username, _password);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_username),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(recipient);
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
