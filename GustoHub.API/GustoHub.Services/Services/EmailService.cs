using GustoHub.Data.ViewModels.GET;
using GustoHub.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace GustoHub.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendAdminApprovalRequestAsync(GETUserDto newUser)
        {
            var emailSettings = configuration.GetSection("EmailSettings");
            var adminEmail = emailSettings["AdminEmail"];
            var senderEmail = emailSettings["SenderEmail"];
            var senderPassword = emailSettings["SenderPassword"];
            var smtpServer = emailSettings["SmtpServer"];
            var smtpPort = int.Parse(emailSettings["SmtpPort"]);

            var subject = "New User Access Request";
            var body = $@"
            A new user has requested access to the platform:
            ID: {newUser.Id}
            Username: {newUser.Username}
            Registered At: {newUser.CreatedAt}

            Please review and approve their access in the admin panel.
        ";

            using var smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                Timeout = 20000
            };

            smtpClient.UseDefaultCredentials = false;


            var mailMessage = new MailMessage(senderEmail, adminEmail, subject, body)
            {
                IsBodyHtml = false, 
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
